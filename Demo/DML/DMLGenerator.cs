using System.Text;

namespace Demo.DML
{
    public class DMLGenerator
    {
        public static String CreateInsertStatement(Object Entity, bool skipPrimaryKey = false)
        {
            var insertStatement = "";
            var sbPropieties = new StringBuilder();
            var entityName = Entity.GetType().Name;
            var sbColumns = new StringBuilder();
            sbColumns.Append("INSERT INTO " + entityName + "(");
            sbPropieties.Append("VALUES (");
            var columId = 0;
            foreach (var propertyInfo in Entity.GetType().GetProperties())
            {
                if(columId != 0)
                {
                    sbColumns.Append(propertyInfo.Name+",");
                    sbPropieties.Append("@" + propertyInfo.Name + ",");
                }
                columId++;
            }
            insertStatement = sbColumns.ToString().Substring(0, sbColumns.ToString().Length - 1) + ")" +
                sbPropieties.ToString().Substring(0, sbPropieties.ToString().Length - 1) + ")";
            return insertStatement;
        }

        public static string UpdateInsertStatement(object Entity, int? id, bool skipPrimaryKey = false)
        {
            var updateStatement = "";
            var sbProperties = new StringBuilder();
            var entityName = Entity.GetType().Name;
            var sbColumns = new StringBuilder();
            sbColumns.Append("UPDATE " + entityName + " SET ");
            var columnId = 0;
            foreach (var propertyInfo in Entity.GetType().GetProperties())
            {
                if (columnId != 0)
                {
                    sbColumns.Append(propertyInfo.Name + " = " + "@" + propertyInfo.Name + ", ");
                }
                columnId++;
            }
            sbProperties.Append(" WHERE id = " + id);
            updateStatement = sbColumns.ToString().Substring(0, sbColumns.ToString().Length - 2) +
            sbProperties.ToString().Substring(0, sbProperties.ToString().Length);
            return updateStatement;
        }
    }
}
