using RestaurantBL.Exceptions;
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Services
{
    public class TableService
    {
        private ITableRepository repo;

        public TableService(ITableRepository repo)
        {
            this.repo = repo;
        }

        public List<Table> GetAllTablesOfRestaurant(int restaurantId)
        {
            List<Table> tables = new List<Table>();
            tables = repo.GetAllTablesOfRestaurant(restaurantId);
            return tables;
        }

        public Table AddTable (Table table)
        {
            try
            {
                if (table == null) throw new RestaurantServiceException("TableService - AddRestaurant - no table entry");
                if (repo.TableExists(table.TableNumber,table.RestaurantId)) throw new RestaurantServiceException("TableService - AddRestaurant - Restaurant already has a tuble with this tablenumber");
                Table tableWithId = repo.AddTable(table);
                return tableWithId;
            }
            catch (RestaurantServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RestaurantServiceException("AddRestaurant", ex);
            }

        }

        public void DeleteTable (int tableId)
        {
            try
            {
                if (!repo.TableExists(tableId)) throw new TableServiceException("tableService - DeleteTable - table doesn't exist");
                repo.DeleteTable(tableId);
            }
            catch (TableServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new TableServiceException("DeleteTable", ex);
            }
        }

        public void UpdateTable (Table table)
        {
            try
            {
                if ( table == null ) throw new TableServiceException("tableService - UpdateTable - no table entry");
                if (!repo.TableExists(table.TableId)) throw new TableServiceException("tableService - UpdateTable - table does not exist");
                Table currentTable = repo.GetTable(table.TableId);
                if (table == currentTable) throw new TableServiceException("tableService - UpdateTable - no different data");
                repo.UpdateTable(table);
            }
            catch (TableServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new TableServiceException("UpdateTable", ex);
            }
        }
    }
}
