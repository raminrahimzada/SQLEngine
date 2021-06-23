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
            var columnsBuilder = New<ColumnsCreateQueryBuilder>();
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

        public override void Build(ISqlWriter writer)
        {
            ValidateAndThrow();

            var cols = _columns.Select(c => c.Model).ToArray();
            writer.Write(C.CREATE);
            writer.Write2(C.TABLE);
            if (!string.IsNullOrEmpty(_schemaName))
            {
                writer.Write(I(_schemaName));
                writer.Write(C.DOT);
            }
            
            writer.Write(I(_tableName));

            writer.Write(C.SPACE);
            writer.Write2(C.BEGIN_SCOPE);
            writer.Indent++;
            writer.WriteLine();

            foreach (var columnQueryBuilder in _columns)
            {
                columnQueryBuilder.Build(writer);
                writer.WriteLine(C.COMMA);
            }
            writer.Indent--;
            writer.Write2(C.END_SCOPE);
            writer.WriteLine(C.SEMICOLON);

            //PK list
            PKList(writer, cols);

            //unique index list
            UXList(writer, cols);

            //FK list
            FKList(writer, cols);

            //default values
            DefaultValues(writer, cols);

            //descriptions
            Descriptions(writer, cols);
            //systemVersioning
            Versioning(writer);
        }

        private void Versioning(ISqlWriter writer)
        {
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

                writer.WriteLine($@"
ALTER TABLE {_schemaName}.{_tableName} 
ADD 
    _START_TIME DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN DEFAULT GETUTCDATE(),
    _END_TIME  DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN DEFAULT CONVERT(DATETIME2, '9999-12-31 23:59:59.9999999'),

PERIOD FOR SYSTEM_TIME (_START_TIME, _END_TIME);");

                writer.WriteLine($"ALTER TABLE {_schemaName}.{_tableName} SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE={_schemaName}.{_logFileName}))");
            }
        }

        private void Descriptions(ISqlWriter writer, ColumnModel[] cols)
        {
            {
                var descriptions = cols.Where(c => !string.IsNullOrEmpty(c.Description)).ToArray();
                if (descriptions.Any())
                {
                    foreach (var model in descriptions)
                    {
                        var t = new ExecuteQueryBuilder();
                        // https://stackoverflow.com/a/3754214/7901692

                        var schemaName = _schemaName;
                        if (string.IsNullOrEmpty(schemaName)) schemaName = "dbo";

                        t.Procedure("sp_addextendedproperty")
                            .Arg("name", "MS_Description".ToSQL())
                            .Arg("value", model.Description.ToSQL())

                            .Arg("level0type", "Schema".ToSQL())
                            .Arg("level0name", schemaName.ToSQL())

                            .Arg("level1type", "Table".ToSQL())
                            .Arg("level1name", _tableName.ToSQL())

                            .Arg("level2type", "Column".ToSQL())
                            .Arg("level2name", model.Name.ToSQL())
                            .Build(writer);
                    }
                }
            }
        }

        private void DefaultValues(ISqlWriter writer, ColumnModel[] cols)
        {
            {
                var defaultValues = cols.Where(c => c.DefaultValue != null).ToArray();

                if (defaultValues.Any())
                {
                    writer.WriteLine();

                    //writer.WriteLineComment($"Default Values List of {_tableName}");

                    foreach (var df in defaultValues)
                    {
                        var defaultConstraintName = df.DefaultConstraintName;
                        if (string.IsNullOrEmpty(defaultConstraintName))
                        {
                            defaultConstraintName = "DF_" + _tableName + df.Name;
                        }

                        writer.Write(C.ALTER);
                        writer.Write2(C.TABLE);
                        writer.Write(I(_tableName));
                        writer.Write2(C.ADD);
                        writer.Write(C.CONSTRAINT);
                        writer.Write2(I(defaultConstraintName));
                        writer.Write(C.DEFAULT);
                        writer.WriteScoped(df.DefaultValue);
                        writer.Write2(C.FOR);
                        writer.Write(I(df.Name));
                        writer.WriteLine(C.SEMICOLON);
                    }
                }
            }
        }

        private void FKList(ISqlWriter writer, ColumnModel[] cols)
        {
            {
                var fkList = cols.Where(c => c.IsForeignKey ?? false).ToArray();
                if (fkList.Any())
                {
                    //writer.WriteLineComment($"Foreign Keys List of {_tableName}");
                    writer.WriteLine();

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
                        writer.Write(C.ALTER);
                        writer.Write2(C.TABLE);
                        writer.Write(I(_tableName));
                        writer.Write2(C.WITH);
                        writer.Write(C.CHECK);
                        writer.Write2(C.ADD);
                        writer.Write(C.CONSTRAINT);
                        writer.Write2(I(fkName));
                        writer.Write(C.FOREIGN);
                        writer.Write2(C.KEY);
                        writer.WriteScoped(I(fk.Name));
                        writer.Write2(C.REFERENCES);
                        writer.Write(I(fk.ForeignKeyTableName));
                        writer.Write(C.BEGIN_SCOPE);
                        writer.Write(C.SPACE);
                        writer.Indent++;
                        writer.Write(I(fk.ForeignKeyColumnName));
                        writer.Write(C.SPACE);
                        writer.Indent--;
                        writer.Write(C.END_SCOPE);

                        writer.WriteLine(C.SEMICOLON);

                        writer.Write(C.ALTER);
                        writer.Write2(C.TABLE);
                        writer.Write(I(_tableName));
                        writer.Write2(C.CHECK);
                        writer.Write(C.CONSTRAINT);
                        writer.Write2(I(fkName));
                        writer.WriteLine(C.SEMICOLON);
                    }
                }
            }
        }

        private void UXList(ISqlWriter writer, ColumnModel[] cols)
        {
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
                    //writer.WriteLineComment($"List of Unique Keys of {_tableName}");

                    foreach (var ukName in ukList)
                    {
                        var ukGroup = cols.Where(c => c.UniqueKeyName == ukName).ToArray();
                        writer.Write(C.ALTER);
                        writer.Write2(C.TABLE);
                        writer.Write(I(_tableName));
                        writer.Write2(C.ADD);
                        writer.Write(C.CONSTRAINT);
                        writer.Write2(I(ukName));
                        writer.Write(C.UNIQUE);
                        writer.Write(C.BEGIN_SCOPE);
                        //writer.Indent++;
                        writer.WriteJoined(ukGroup.Select(pkg => pkg.Name + C.SPACE +
                                                                 (pkg.IsUniqueKeyOrderDescending ? C.DESC : C.ASC))
                            .ToArray());


                        //writer.Indent--;
                        writer.Write(C.END_SCOPE);
                        writer.WriteLine(C.SEMICOLON);
                    }
                }
            }
        }

        private void PKList(ISqlWriter writer, ColumnModel[] cols)
        {
            {
                var pkList = cols.Where(c => c.IsPrimary ?? false).ToArray();
                if (pkList.Any())
                {
                    var pkGroups = pkList.GroupBy(pk => pk.PrimaryKeyName).ToArray();
                    if (pkGroups.Any())
                    {
                        //writer.WriteLineComment($"Primary Keys List {_tableName}");
                        writer.WriteLine();
                    }
                    foreach (var pkGroup in pkGroups)
                    {
                        var pkName = pkGroup.Key;
                        if (string.IsNullOrEmpty(pkName))
                        {
                            pkName = "PK_" + _tableName + "_" +
                                     pkList.FirstOrDefault(x => x.PrimaryKeyName == pkGroup.Key)?.Name;
                        }
                        writer.Write(C.ALTER);
                        writer.Write2(C.TABLE);
                        writer.Write(I(_tableName));
                        writer.Write2(C.ADD);
                        writer.Write(C.CONSTRAINT);
                        writer.Write2(I(pkName));
                        writer.Write(C.PRIMARY);
                        writer.Write2(C.KEY);
                        writer.Write(C.CLUSTERED);
                        writer.Write2(C.BEGIN_SCOPE);
                        writer.Indent++;
                        writer.WriteJoined(pkGroup.Select(pkg => I(pkg.Name)).ToArray());
                        writer.Indent--;
                        writer.Write(C.END_SCOPE);
                        writer.Write(C.SEMICOLON);
                    }
                }
                writer.WriteLine();
            }
        }

        public ICreateTableQueryBuilder SystemVersioned(bool systemVersioning, string logFileName = null)
        {
            _systemVersioning = systemVersioning;
            _logFileName = logFileName;
            return this;
        }
    }
}