using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using EasyElements.Configs;

namespace EasyElements
{
    public class ElementsData
    {
        public short Version { get; set; }
        public DataSet Data { get; protected set; }
        public Config ConfigForThisElements { get; }

        internal Dictionary<ElementsList, List<byte[]>> SkipValues { get; set; }
        internal short Segmentation { get; set; }

        internal ElementsData(short version, short segmentation, DataSet data, Dictionary<ElementsList, List<byte[]>> skipValues, Config configForThisElements)
        {
            this.SkipValues = skipValues;
            this.ConfigForThisElements = configForThisElements;
            this.Data = data;
            this.Version = version;
            this.Segmentation = segmentation;
        }

        #region GetAllRows

        /// <summary>
        /// It returns all rows from all lists
        /// </summary>
        /// <returns>All rows</returns>
        public IEnumerable<DataRow> GetAllRows() =>
            GetAllRows(ConfigForThisElements.Lists);

        /// <summary>
        /// It returns all rows from these list
        /// </summary>
        /// <param name="list">these lists</param>
        /// <returns>All rows</returns>
        public IEnumerable<DataRow> GetAllRows(ElementsList list) =>
             Data.Tables[list.Name].Rows.Cast<DataRow>();

        /// <summary>
        /// It returns all rows from these lists
        /// </summary>
        /// <param name="lists">these lists</param>
        /// <returns>All rows</returns>
        public IEnumerable<DataRow> GetAllRows(IEnumerable<ElementsList> lists) =>
             lists.SelectMany(x => Data.Tables[x.Name].Rows.Cast<DataRow>());

        #endregion

        #region FindByID

        /// <summary>
        /// Search row on a unique identifier in all lists
        /// </summary>
        /// <param name="ID">Unique identifier</param>
        /// <returns>Found row</returns>
        public DataRow FindByID(int ID) =>
            FindByID(ID, ConfigForThisElements.Lists);

        /// <summary>
        /// Search row on a unique identifier in the list
        /// </summary>
        /// <param name="ID">unique identifier</param>
        /// <param name="list">list</param>
        /// <returns>Found row</returns>
        public DataRow FindByID(int ID, ElementsList list) =>
            GetAllRows(list).FirstOrDefault(dataRow => dataRow["ID"].Equals(ID));

        /// <summary>
        /// Search row on a unique identifier in the lists
        /// </summary>
        /// <param name="ID">unique identifier</param>
        /// <param name="lists">these lists</param>
        /// <returns>Found row</returns>
        public DataRow FindByID(int ID, IEnumerable<ElementsList> lists) =>
            GetAllRows(lists).FirstOrDefault(dataRow => dataRow["ID"].Equals(ID));

        #endregion

        #region FindByName

        /// <summary>
        /// Search row by name in all lists
        /// </summary>
        /// <param name="Name">Item name</param>
        /// <returns>Found row</returns>
        public DataRow FindByName(string Name) =>
            FindByName(Name, ConfigForThisElements.Lists);

        /// <summary>
        /// Search row by name in the list
        /// </summary>
        /// <param name="Name">Item name</param>
        /// <param name="list">list</param>
        /// <returns>Found row</returns>
        public DataRow FindByName(string Name, ElementsList list) =>
           GetAllRows(list).FirstOrDefault(dataRow => dataRow["Name"].Equals(Name));

        /// <summary>
        /// Search row by name in the lists
        /// </summary>
        /// <param name="Name">Item name</param>
        /// <param name="lists">these lists</param>
        /// <returns>Found row</returns>
        public DataRow FindByName(string Name, IEnumerable<ElementsList> lists) =>
           GetAllRows(lists).FirstOrDefault(dataRow => dataRow["Name"].Equals(Name));

        #endregion

        #region FindByFieldName

        /// <summary>
        /// Search row by field name in all lists
        /// </summary>
        /// <param name="Name">Item name</param>
        /// <param name="FieldName"></param>
        /// <returns>Found row</returns>
        public DataRow FindByFieldName(string Name, string FieldName) =>
            FindByName(Name, ConfigForThisElements.Lists);

        /// <summary>
        /// Search row by field name in the list
        /// </summary>
        /// <param name="Name">Item name</param>
        /// <param name="FieldName"></param>
        /// <param name="list">list</param>
        /// <returns>Found row</returns>
        public DataRow FindByFieldName(string Name, string FieldName, ElementsList list) =>
           GetAllRows(list).FirstOrDefault(dataRow => dataRow[FieldName].Equals(Name));

        /// <summary>
        /// Search row by field name in the lists
        /// </summary>
        /// <param name="Name">Item name</param>
        /// <param name="FieldName"></param>
        /// <param name="lists">these lists</param>
        /// <returns>Found row</returns>
        public DataRow FindByFieldName(string Name, string FieldName, IEnumerable<ElementsList> lists) =>
           GetAllRows(lists).FirstOrDefault(dataRow => dataRow[FieldName].Equals(Name));

        #endregion

        #region GetFreeID
        /// <summary>
        /// Search unique identifier among all lists
        /// If the ID is not found - will return -1
        /// </summary>
        /// <param name="MinID">Begin ID</param>
        /// <param name="MaxID">finish ID</param>
        /// <returns>unique identifier or -1</returns>
        public int GetFreeID(int MinID, int MaxID) =>
            GetFreeID(GetAllRows(ConfigForThisElements.Lists), MinID, MaxID);

        /// <summary>
        /// Search unique identifier in the list
        /// If the ID is not found - will return -1
        /// </summary>
        /// <param name="list">there list</param>
        /// <param name="MinID">Begin ID</param>
        /// <param name="MaxID">finish ID</param>
        /// <returns>unique identifier or -1</returns>
        public int GetFreeID(ElementsList list, int MinID, int MaxID) =>
            GetFreeID(GetAllRows(list), MinID, MaxID);

        /// <summary>
        /// Search unique identifier among these lists
        /// If the ID is not found - will return -1
        /// </summary>
        /// <param name="lists">lists</param>
        /// <param name="MinID">Begin ID</param>
        /// <param name="MaxID">finish ID</param>
        /// <returns>unique identifier or -1</returns>
        public int GetFreeID(IEnumerable<ElementsList> lists, int MinID, int MaxID) =>
            GetFreeID(GetAllRows(lists), MinID, MaxID);

        /// <summary>
        /// Search unique identifier among these rows
        /// If the ID is not found - will return -1
        /// </summary>
        /// <param name="rows">there rows</param>
        /// <param name="MinID">Begin ID</param>
        /// <param name="MaxID">finish ID</param>
        /// <returns>unique identifier or -1</returns>
        public int GetFreeID(IEnumerable<DataRow> rows, int MinID, int MaxID)
        {
            var ids = rows.Select(x => (int) x["ID"]).ToArray();

            for (var i = MinID; i < MaxID; i++)
                if (!ids.Contains(i))
                    return i;

            return -1;
        }
    }
        #endregion


    
}