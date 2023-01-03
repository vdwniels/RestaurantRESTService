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

        public Table GetTable(int tableId)
        {
            try
            {
                if (!repo.TableExists(tableId)) throw new TableServiceException("TableService - GetTable - no table entry");
                return repo.GetTable(tableId);
            }
            catch (UserServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new UserServiceException("GetTable", ex);
            }
        }

        public Table AddTable (Table table)
        {
            try
            {
                if (table == null) throw new TableServiceException("TableService - AddTable - no table entry");
                if (repo.TableExists(table.TableNumber,table.RestaurantId)) throw new TableServiceException("TableService - AddTable - Table already exists");
                Table tableWithId = repo.AddTable(table);
                return tableWithId;
            }
            catch (RestaurantServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RestaurantServiceException("AddTabl", ex);
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
                if (repo.TableExists(table.TableNumber, table.RestaurantId)) throw new TableServiceException("TableService - AddTable - Table with this tablenumber already exists");
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
