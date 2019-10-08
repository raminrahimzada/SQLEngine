using System;
using System.Collections.Generic;
using System.Linq;
using SQLEngine.Helpers;
using static SQLEngine.SQLKeywords;

namespace SQLEngine.Builders
{
    public class CreateTableQueryBuilder : AbstractQueryBuilder
    {
        private string _tableName;
        private readonly List<ColumnQueryBuilder> _columns;

        public CreateTableQueryBuilder()
        {
            _columns=new List<ColumnQueryBuilder>();
        }
        public CreateTableQueryBuilder Name(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public CreateTableQueryBuilder ResetColumns()
        {
            _columns.Clear();
            return this;
        }

        public CreateTableQueryBuilder Columns(Func<ColumnsQueryBuilder, ColumnQueryBuilder[]> action)
        {
            var columnsBuilder = GetDefault<ColumnsQueryBuilder>();
            var current = action(columnsBuilder);
            _columns.AddRange(current);
            return this;
        }

        public string RandomString => Guid.NewGuid().ToString().Replace("-", "").Substring(0, 5);

        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            //only one identity is allowed in table
            var count = _columns.Count(c => c.BuildModel().IsIdentity ?? false);
            if (count > 1)
            {
                Boom();
            }
        }

        public override string Build()
        {
            ValidateAndThrow();

            var cols = _columns.Select(c => c.BuildModel()).ToArray();
            Writer.Write(CREATE);
            Writer.Write2(TABLE);
            Writer.Write2(I(_tableName));
            Writer.Write2(BEGIN_SCOPE);
            Writer.Indent++;
            Writer.WriteLine();

            foreach (var columnQueryBuilder in _columns)
            {
                var columnQuery = columnQueryBuilder.Build();
                Writer.Write(columnQuery);
                Writer.WriteLine(COMMA);
            }
            Writer.Indent--;
            Writer.Write2(END_SCOPE);
            Writer.WriteLine();

            //PK list
            {
                var pkList = cols.Where(c => c.IsPrimary ?? false).ToArray();
                if (pkList.Any())
                {
                    var pkGroups = pkList.GroupBy(pk => pk.PrimaryKeyName).ToArray();
                    if (pkGroups.Any())
                    {
                        Writer.WriteLineComment($"Primary Keys List {_tableName}");
                        Writer.WriteLine();
                    }
                    foreach (var pkGroup in pkGroups)
                    {
                        var pkName = pkGroup.Key;
                        if (string.IsNullOrEmpty(pkName))
                        {
                            pkName = "PK_" + _tableName + "_" + RandomString;
                        }
                        Writer.Write(ALTER);
                        Writer.Write2(TABLE);
                        Writer.Write2(I(_tableName));
                        Writer.Write2(ADD);
                        Writer.Write2(CONSTRAINT);
                        Writer.Write2(I(pkName));
                        Writer.Write2(PRIMARY);
                        Writer.Write2(KEY);
                        Writer.Write2(CLUSTERED);
                        Writer.WriteLine();
                        Writer.Write2(BEGIN_SCOPE);
                        Writer.WriteLine();
                        Writer.Indent++;
                        Writer.WriteLineJoined(pkGroup.Select(pkg => I(pkg.Name)).ToArray());
                        Writer.Indent--;
                        Writer.Write2(END_SCOPE);
                        Writer.Write(WITH);
                        Writer.WriteScoped(DEFAULT_PK_OPTIONS);
                        Writer.Write2(ON);
                        Writer.WriteScoped(PRIMARY, BEGIN_SQUARE, END_SQUARE);
                        Writer.WriteLine();
                    }
                }
                Writer.WriteLine();
            }

            //unique index list
            {
               
                
                foreach (var t in cols.Where(c => c.IsUniqueKey ?? false))
                {
                    if (string.IsNullOrEmpty(t.UniqueKeyName))
                    {
                        t.UniqueKeyName = "IX_" + _tableName + "_" + RandomString;
                    }
                }

                var ukList = cols.Where(c => c.IsUniqueKey ?? false).Select(c => c.UniqueKeyName)
                    .Distinct()
                    .ToArray();

                if (ukList.Any())
                {

                    Writer.WriteLineComment($"List of Unique Keys of {_tableName}");

                    foreach (var ukName in ukList)
                    {
                        var ukGroup = cols.Where(c => c.UniqueKeyName == ukName).ToArray();
                        Writer.Write(ALTER);
                        Writer.Write2(TABLE);
                        Writer.Write2(I(_tableName));
                        Writer.Write2(ADD);
                        Writer.Write2(CONSTRAINT);
                        Writer.Write2(I(ukName));
                        Writer.Write2(UNIQUE);
                        Writer.Write2(NONCLUSTERED);
                        Writer.WriteLine();
                        Writer.Write2(BEGIN_SCOPE);
                        Writer.Indent++;
                        Writer.WriteLine();
                        Writer.WriteLineJoined(ukGroup.Select(pkg => pkg.Name + SPACE +
                                                                     (pkg.IsUniqueKeyOrderDescending ? DESC : ASC))
                            .ToArray());


                        Writer.Indent--;
                        Writer.Write2(END_SCOPE);
                        Writer.Write(WITH);
                        Writer.WriteScoped(DEFAULT_PK_OPTIONS);
                        Writer.Write2(ON);
                        Writer.WriteScoped(PRIMARY, BEGIN_SQUARE, END_SQUARE);
                        Writer.WriteLine();

                    }
                    Writer.WriteLine();
                }
                Writer.WriteLine();
            }

            //FK list

            {
                var fkList = cols.Where(c => c.IsForeignKey ?? false).ToArray();
                if (fkList.Any())
                {
                    Writer.WriteLineComment($"Foreign Keys List of {_tableName}");
                    Writer.WriteLine();

                    foreach (var fk in fkList)
                    {
                        var fkName = fk.ForeignKeyConstraintName;
                        if (string.IsNullOrEmpty(fkName))
                        {
                            fkName = "FK_" + _tableName + fk.ForeignKeyColumnName + "_" + RandomString;
                        }

                        Writer.Write(ALTER);
                        Writer.Write2(TABLE);
                        Writer.Write(I(_tableName));
                        Writer.Write2(WITH);
                        Writer.Write2(CHECK);
                        Writer.Write2(ADD);
                        Writer.Write2(CONSTRAINT);
                        Writer.Write2(I(fkName));
                        Writer.Write2(FOREIGN);
                        Writer.Write2(KEY);
                        Writer.WriteScoped(I(fk.Name));
                        Writer.Write2(REFERENCES);
                        Writer.WriteLine(I(fk.ForeignKeyTableName));
                        Writer.Write(BEGIN_SCOPE);
                        Writer.WriteLine();
                        Writer.Indent++;
                        Writer.Write(I(fk.ForeignKeyColumnName));
                        Writer.WriteLine();
                        Writer.Indent--;
                        Writer.Write(END_SCOPE);

                        Writer.WriteLine();

                        Writer.Write(ALTER);
                        Writer.Write2(TABLE);
                        Writer.Write(I(_tableName));
                        Writer.Write2(CHECK);
                        Writer.Write2(CONSTRAINT);
                        Writer.Write2(I(fkName));
                        Writer.WriteLine();
                    }
                }
            }

            //default values
            {
                var defaultValues = cols.Where(c => c.DefaultValue != null).ToArray();

                if (defaultValues.Any())
                {
                    Writer.WriteLine();

                    Writer.WriteLineComment($"Default Values List of {_tableName}");

                    foreach (var df in defaultValues)
                    {
                        var defaultConstraintName = df.DefaultConstraintName;
                        if (string.IsNullOrEmpty(defaultConstraintName))
                        {
                            defaultConstraintName = "DF_" + _tableName + df.ForeignKeyColumnName + "_" + RandomString;
                        }

                        Writer.Write(ALTER);
                        Writer.Write2(TABLE);
                        Writer.Write(I(_tableName));
                        Writer.Write2(ADD);
                        Writer.Write2(CONSTRAINT);
                        Writer.Write2(I(defaultConstraintName));
                        Writer.Write2(DEFAULT);
                        Writer.WriteScoped(df.DefaultValue);
                        Writer.Write2(FOR);
                        Writer.WriteLine(I(df.Name));
                        Writer.WriteLine();
                    }
                }
            }

            return base.Build();
        }
    }
}