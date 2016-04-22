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
             Data.Tables[list.Name].Select();

        /// <summary>
        /// It returns all rows from these lists
        /// </summary>
        /// <param name="lists">these lists</param>
        /// <returns>All rows</returns>
        public IEnumerable<DataRow> GetAllRows(IEnumerable<ElementsList> lists) =>
             lists.SelectMany(x => Data.Tables[x.Name].Select());

        #endregion

        #region GetFreeID

        private int lastUniqueID = 1;
        /// <summary>
        /// Search unique identifier among all lists
        /// If the ID is not found - will return -1
        /// </summary>
        /// <param name="MaxID">finish ID</param>
        /// <returns>unique identifier or -1</returns>
        public int GetFreeID(int MaxID) =>
            GetFreeID(GetAllRows(ConfigForThisElements.Lists), MaxID);

        /// <summary>
        /// Search unique identifier in the list
        /// If the ID is not found - will return -1
        /// </summary>
        /// <param name="list">there list</param>
        /// <param name="MaxID">finish ID</param>
        /// <returns>unique identifier or -1</returns>
        public int GetFreeID(ElementsList list, int MaxID) =>
            GetFreeID(GetAllRows(list), MaxID);

        /// <summary>
        /// Search unique identifier among these lists
        /// If the ID is not found - will return -1
        /// </summary>
        /// <param name="lists">lists</param>
        /// <param name="MaxID">finish ID</param>
        /// <returns>unique identifier or -1</returns>
        public int GetFreeID(IEnumerable<ElementsList> lists, int MaxID) =>
            GetFreeID(GetAllRows(lists), MaxID);

        /// <summary>
        /// Search unique identifier among these rows
        /// If the ID is not found - will return -1
        /// </summary>
        /// <param name="rows">there rows</param>
        /// <param name="MaxID">finish ID</param>
        /// <returns>unique identifier or -1</returns>
        public int GetFreeID(IEnumerable<DataRow> rows, int MaxID)
        {
            var ids = rows.Select(x => x["ID"]).ToArray();

            for (var i = lastUniqueID; i < MaxID; i++)
                if (!ids.Contains(i))
                {
                    lastUniqueID = i;
                    return i;
                }

            return -1;
        }
        #endregion

    }
}