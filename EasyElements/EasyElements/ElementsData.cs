using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EasyElements.Configs;

namespace EasyElements
{
    public class ElementsData
    {
        public short Version { get; set; }
        public DataSet Data { get; protected set; }
        public Config ConfigForThisElements { get; private set; }

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

        /// <summary>
        /// It returns all rows from these lists
        /// </summary>
        /// <param name="lists">these lists</param>
        /// <returns>All rows</returns>
        public IEnumerable<DataRow> GetAllRowsByLists(List<ElementsList> lists) =>
             lists.SelectMany(x => Data.Tables[x.Name].Rows.Cast<DataRow>());

        /// <summary>
        /// Search row on a unique identifier in all list
        /// </summary>
        /// <param name="ID">Unique identifier</param>
        /// <returns>Found row</returns>
        public DataRow FindByID(int ID) =>
            FindByID(ID, ConfigForThisElements.Lists);

        /// <summary>
        /// Search row on a unique identifier in the list
        /// </summary>
        /// <param name="ID">unique identifier</param>
        /// <param name="lists">these lists</param>
        /// <returns>Found row</returns>
        public DataRow FindByID(int ID, List<ElementsList> lists) =>
            GetAllRowsByLists(lists).FirstOrDefault(dataRow => dataRow["ID"].Equals(ID));

        /// <summary>
        /// Search row by name in all lists
        /// </summary>
        /// <param name="Name">Item name</param>
        /// <returns>Found row</returns>
        public DataRow FindByName(string Name) =>
            FindByName(Name, ConfigForThisElements.Lists);

        /// <summary>
        /// Search row by name in the lists
        /// </summary>
        /// <param name="Name">Item name</param>
        /// <param name="lists">these lists</param>
        /// <returns>Found row</returns>
        public DataRow FindByName(string Name, List<ElementsList> lists) =>
           GetAllRowsByLists(lists).FirstOrDefault(dataRow => dataRow["Name"].Equals(Name));
        


    }
}