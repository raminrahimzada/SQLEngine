using System;
using System.Collections.Generic;
using System.Linq;

namespace SQLEngine.SqlServer
{
    internal class CreateTableQueryBuilder : AbstractQueryBuilder, ICreateTableQueryBuilder
    {
        private string _tableName;
        private string _schemaName;
        private readonly List<IColumnQueryBuilder> _columns;
        private bool _systemVersioning;
        private string _logFileName;

        public CreateTableQueryBuilder()
        {
            _columns = new List<IColumnQueryBuilder>();
        }
        public ICreateTableQueryBuilder Name(string tableName)
        {
            _tableName = tableName;
            return this;
        }
        public ICreateTableQueryBuilder Schema(string schemaName)
        {
            _schemaName = schemaName;
            return this;
        }

        public ICreateTableQueryBuilder ResetColumns()
        {
            _columns.Clear();
            return this;
        }

        public ICreateTableQueryBuilder Columns(Func<IColumnsCreateQueryBuilder, IColumnQueryBuilder[]> action)
        {
            var columnsBuilder = GetDefault<ColumnsCreateQueryBuilder>();
            var current = action(columnsBuilder);
            _columns.AddRange(current);
            return this;
        }


        protected override void ValidateAndThrow()
        {
            base.ValidateAndThrow();
            //only one identity is allowed in table
            var count = _columns.Count(c => c.Model.IsIdentity ?? false);
            if (count > 1)
            {
                Bomb();
            }
        }

        public override string Build()
        {
            ValidateAndThrow();

            var cols = _columns.Select(c => c.Model).ToArray();
            Writer.Write(C.CREATE);
            Writer.Write2(C.TABLE);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                Writer.Write(I(_schemaName));
                Writer.Write(C.DOT);
            }
            
            Writer.Write(I(_tableName));

            Writer.Write(C.SPACE);
            Writer.Write2(C.BEGIN_SCOPE);
            Writer.Indent++;
            Writer.WriteLine();

            foreach (var columnQueryBuilder in _columns)
            {
                var columnQuery = columnQueryBuilder.Build();
                Writer.Write(columnQuery);
                Writer.WriteLine(C.COMMA);
            }
            Writer.Indent--;
            Writer.Write2(C.END_SCOPE);
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
                            pkName = "PK_" + _tableName + "_" +
                                     pkList.FirstOrDefault(x => x.PrimaryKeyName == pkGroup.Key)?.Name;
                            ;
                        }
                        Writer.Write(C.ALTER);
                        Writer.Write2(C.TABLE);
                        Writer.Write2(I(_tableName));
                        Writer.Write2(C.ADD);
                        Writer.Write2(C.CONSTRAINT);
                        Writer.Write2(I(pkName));
                        Writer.Write2(C.PRIMARY);
                        Writer.Write2(C.KEY);
                        Writer.Write2(C.CLUSTERED);
                        Writer.WriteLine();
                        Writer.Write2(C.BEGIN_SCOPE);
                        Writer.WriteLine();
                        Writer.Indent++;
                        Writer.WriteLineJoined(pkGroup.Select(pkg => I(pkg.Name)).ToArray());
                        Writer.Indent--;
                        Writer.Write2(C.END_SCOPE);
                        Writer.Write(C.WITH);
                        Writer.WriteScoped(C.DEFAULT_PK_OPTIONS);
                        Writer.Write2(C.ON);
                        Writer.WriteScoped(C.PRIMARY, C.BEGIN_SQUARE, C.END_SQUARE);
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
                        t.UniqueKeyName = "IX_" + _tableName + "_" + t.Name;
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
                        Writer.Write(C.ALTER);
                        Writer.Write2(C.TABLE);
                        Writer.Write2(I(_tableName));
                        Writer.Write2(C.ADD);
                        Writer.Write2(C.CONSTRAINT);
                        Writer.Write2(I(ukName));
                        Writer.Write2(C.UNIQUE);
                        Writer.Write2(C.NONCLUSTERED);
                        Writer.WriteLine();
                        Writer.Write2(C.BEGIN_SCOPE);
                        Writer.Indent++;
                        Writer.WriteLine();
                        Writer.WriteLineJoined(ukGroup.Select(pkg => pkg.Name + C.SPACE +
                                                                     (pkg.IsUniqueKeyOrderDescending ? C.DESC : C.ASC))
                            .ToArray());


                        Writer.Indent--;
                        Writer.Write2(C.END_SCOPE);
                        Writer.Write(C.WITH);
                        Writer.WriteScoped(C.DEFAULT_PK_OPTIONS);
                        Writer.Write2(C.ON);
                        Writer.WriteScoped(C.PRIMARY, C.BEGIN_SQUARE, C.END_SQUARE);
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
                            fkName = "FK_" + I(_tableName) + "_" + fk.Name + "__" + I(fk.ForeignKeyTableName) + "_" +
                                     fk.ForeignKeyColumnName;
                        }

                        fkName = fkName
                            .Replace(".", "")
                            .Replace("[", "")
                            .Replace("]", "")
                            ;
                        Writer.Write(C.ALTER);
                        Writer.Write2(C.TABLE);
                        Writer.Write(I(_tableName));
                        Writer.Write2(C.WITH);
                        Writer.Write2(C.CHECK);
                        Writer.Write2(C.ADD);
                        Writer.Write2(C.CONSTRAINT);
                        Writer.Write2(I(fkName));
                        Writer.Write2(C.FOREIGN);
                        Writer.Write2(C.KEY);
                        Writer.WriteScoped(I(fk.Name));
                        Writer.Write2(C.REFERENCES);
                        Writer.WriteLine(I(fk.ForeignKeyTableName));
                        Writer.Write(C.BEGIN_SCOPE);
                        Writer.WriteLine();
                        Writer.Indent++;
                        Writer.Write(I(fk.ForeignKeyColumnName));
                        Writer.WriteLine();
                        Writer.Indent--;
                        Writer.Write(C.END_SCOPE);

                        Writer.WriteLine();

                        Writer.Write(C.ALTER);
                        Writer.Write2(C.TABLE);
                        Writer.Write(I(_tableName));
                        Writer.Write2(C.CHECK);
                        Writer.Write2(C.CONSTRAINT);
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
                            defaultConstraintName = "DF_" + _tableName + df.Name;
                        }

                        Writer.Write(C.ALTER);
                        Writer.Write2(C.TABLE);
                        Writer.Write(I(_tableName));
                        Writer.Write2(C.ADD);
                        Writer.Write2(C.CONSTRAINT);
                        Writer.Write2(I(defaultConstraintName));
                        Writer.Write2(C.DEFAULT);
                        Writer.WriteScoped(df.DefaultValue);
                        Writer.Write2(C.FOR);
                        Writer.WriteLine(I(df.Name));
                        Writer.WriteLine();
                    }
                }
            }

            //descriptions
            {
                var descriptions = cols.Where(c => !string.IsNullOrEmpty(c.Description)).ToArray();
                if (descriptions.Any())
                {
                    foreach (var model in descriptions)
                    {
                        using (var t = new ExecuteQueryBuilder())
                        {
                            // https://stackoverflow.com/a/3754214/7901692

                            var schemaName = _schemaName;
                            if (string.IsNullOrEmpty(schemaName)) schemaName = "dbo";
                            //TODO
                            throw new NotImplementedException();
                            //var query = t.Procedure("sp_addextendedproperty")
                            //    .Arg("name", "MS_Description".ToSQL())
                            //    .Arg("value", model.Description.ToSQL())

                            //    .Arg("level0type", "Schema".ToSQL())
                            //    .Arg("level0name", schemaName.ToSQL())

                            //    .Arg("level1type", "Table".ToSQL())
                            //    .Arg("level1name", _tableName.ToSQL())

                            //    .Arg("level2type", "Column".ToSQL())
                            //    .Arg("level2name", model.Name.ToSQL())
                            //    .Build();

                            //Writer.Write(query);
                        }
                    }
                }
            }
            //systemVersioning
            if (_systemVersioning)
            {
                if (string.IsNullOrEmpty(_schemaName))
                {
                    _schemaName = "dbo";
                }
                if (string.IsNullOrEmpty(_logFileName))
                {
                    _logFileName = _tableName + "__LOG";
                }

                Writer.WriteLine($@"
ALTER TABLE {_schemaName}.{_tableName} 
ADD 
    _START_TIME DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN DEFAULT GETUTCDATE(),
    _END_TIME  DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN DEFAULT CONVERT(DATETIME2, '9999-12-31 23:59:59.9999999'),

PERIOD FOR SYSTEM_TIME (_START_TIME, _END_TIME);");

                Writer.WriteLine($"ALTER TABLE {_schemaName}.{_tableName} SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE={_schemaName}.{_logFileName}))");
            }
            return base.Build();
        }

        public ICreateTableQueryBuilder SystemVersioned(bool systemVersioning, string logFileName = null)
        {
            _systemVersioning = systemVersioning;
            _logFileName = logFileName;
            return this;
        }
    }
}