using System;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Radzen;

using QCRM.Data;

namespace QCRM
{
    public partial class DB_157005_crm7desService
    {
        DB_157005_crm7desContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly DB_157005_crm7desContext context;
        private readonly NavigationManager navigationManager;

        public DB_157005_crm7desService(DB_157005_crm7desContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public void ApplyQuery<T>(ref IQueryable<T> items, Query query = null)
        {
            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }
        }


        public async Task ExportActividadesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/actividades/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/actividades/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportActividadesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/actividades/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/actividades/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnActividadesRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Actividades> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Actividades>> GetActividades(Query query = null)
        {
            var items = Context.Actividades.AsQueryable();

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Tiposact);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnActividadesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnActividadesGet(QCRM.Models.DB_157005_crm7des.Actividades item);
        partial void OnGetActividadesByIdActividad(ref IQueryable<QCRM.Models.DB_157005_crm7des.Actividades> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Actividades> GetActividadesByIdActividad(int idactividad)
        {
            var items = Context.Actividades
                              .AsNoTracking()
                              .Where(i => i.ID_ACTIVIDAD == idactividad);

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Tiposact);
            items = items.Include(i => i.Usuarios);
 
            OnGetActividadesByIdActividad(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnActividadesGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnActividadesCreated(QCRM.Models.DB_157005_crm7des.Actividades item);
        partial void OnAfterActividadesCreated(QCRM.Models.DB_157005_crm7des.Actividades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Actividades> CreateActividades(QCRM.Models.DB_157005_crm7des.Actividades actividades)
        {
            OnActividadesCreated(actividades);

            var existingItem = Context.Actividades
                              .Where(i => i.ID_ACTIVIDAD == actividades.ID_ACTIVIDAD)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Actividades.Add(actividades);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(actividades).State = EntityState.Detached;
                throw;
            }

            OnAfterActividadesCreated(actividades);

            return actividades;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Actividades> CancelActividadesChanges(QCRM.Models.DB_157005_crm7des.Actividades item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnActividadesUpdated(QCRM.Models.DB_157005_crm7des.Actividades item);
        partial void OnAfterActividadesUpdated(QCRM.Models.DB_157005_crm7des.Actividades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Actividades> UpdateActividades(int idactividad, QCRM.Models.DB_157005_crm7des.Actividades actividades)
        {
            OnActividadesUpdated(actividades);

            var itemToUpdate = Context.Actividades
                              .Where(i => i.ID_ACTIVIDAD == actividades.ID_ACTIVIDAD)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(actividades);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterActividadesUpdated(actividades);

            return actividades;
        }

        partial void OnActividadesDeleted(QCRM.Models.DB_157005_crm7des.Actividades item);
        partial void OnAfterActividadesDeleted(QCRM.Models.DB_157005_crm7des.Actividades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Actividades> DeleteActividades(int idactividad)
        {
            var itemToDelete = Context.Actividades
                              .Where(i => i.ID_ACTIVIDAD == idactividad)
                              .Include(i => i.Documentos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnActividadesDeleted(itemToDelete);


            Context.Actividades.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterActividadesDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCambioToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/cambio/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/cambio/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCambioToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/cambio/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/cambio/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCambioRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Cambio> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Cambio>> GetCambio(Query query = null)
        {
            var items = Context.Cambio.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCambioRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCambioGet(QCRM.Models.DB_157005_crm7des.Cambio item);
        partial void OnGetCambioByFecha(ref IQueryable<QCRM.Models.DB_157005_crm7des.Cambio> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Cambio> GetCambioByFecha(DateTime fecha)
        {
            var items = Context.Cambio
                              .AsNoTracking()
                              .Where(i => i.FECHA == fecha);

 
            OnGetCambioByFecha(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCambioGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCambioCreated(QCRM.Models.DB_157005_crm7des.Cambio item);
        partial void OnAfterCambioCreated(QCRM.Models.DB_157005_crm7des.Cambio item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cambio> CreateCambio(QCRM.Models.DB_157005_crm7des.Cambio cambio)
        {
            OnCambioCreated(cambio);

            var existingItem = Context.Cambio
                              .Where(i => i.FECHA == cambio.FECHA)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Cambio.Add(cambio);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cambio).State = EntityState.Detached;
                throw;
            }

            OnAfterCambioCreated(cambio);

            return cambio;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Cambio> CancelCambioChanges(QCRM.Models.DB_157005_crm7des.Cambio item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCambioUpdated(QCRM.Models.DB_157005_crm7des.Cambio item);
        partial void OnAfterCambioUpdated(QCRM.Models.DB_157005_crm7des.Cambio item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cambio> UpdateCambio(DateTime fecha, QCRM.Models.DB_157005_crm7des.Cambio cambio)
        {
            OnCambioUpdated(cambio);

            var itemToUpdate = Context.Cambio
                              .Where(i => i.FECHA == cambio.FECHA)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cambio);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCambioUpdated(cambio);

            return cambio;
        }

        partial void OnCambioDeleted(QCRM.Models.DB_157005_crm7des.Cambio item);
        partial void OnAfterCambioDeleted(QCRM.Models.DB_157005_crm7des.Cambio item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cambio> DeleteCambio(DateTime fecha)
        {
            var itemToDelete = Context.Cambio
                              .Where(i => i.FECHA == fecha)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCambioDeleted(itemToDelete);


            Context.Cambio.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCambioDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCiudadesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/ciudades/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/ciudades/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCiudadesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/ciudades/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/ciudades/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCiudadesRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Ciudades> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Ciudades>> GetCiudades(Query query = null)
        {
            var items = Context.Ciudades.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCiudadesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCiudadesGet(QCRM.Models.DB_157005_crm7des.Ciudades item);
        partial void OnGetCiudadesByCiudad(ref IQueryable<QCRM.Models.DB_157005_crm7des.Ciudades> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Ciudades> GetCiudadesByCiudad(string ciudad)
        {
            var items = Context.Ciudades
                              .AsNoTracking()
                              .Where(i => i.CIUDAD == ciudad);

 
            OnGetCiudadesByCiudad(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCiudadesGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCiudadesCreated(QCRM.Models.DB_157005_crm7des.Ciudades item);
        partial void OnAfterCiudadesCreated(QCRM.Models.DB_157005_crm7des.Ciudades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ciudades> CreateCiudades(QCRM.Models.DB_157005_crm7des.Ciudades ciudades)
        {
            OnCiudadesCreated(ciudades);

            var existingItem = Context.Ciudades
                              .Where(i => i.CIUDAD == ciudades.CIUDAD)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Ciudades.Add(ciudades);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(ciudades).State = EntityState.Detached;
                throw;
            }

            OnAfterCiudadesCreated(ciudades);

            return ciudades;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Ciudades> CancelCiudadesChanges(QCRM.Models.DB_157005_crm7des.Ciudades item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCiudadesUpdated(QCRM.Models.DB_157005_crm7des.Ciudades item);
        partial void OnAfterCiudadesUpdated(QCRM.Models.DB_157005_crm7des.Ciudades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ciudades> UpdateCiudades(string ciudad, QCRM.Models.DB_157005_crm7des.Ciudades ciudades)
        {
            OnCiudadesUpdated(ciudades);

            var itemToUpdate = Context.Ciudades
                              .Where(i => i.CIUDAD == ciudades.CIUDAD)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(ciudades);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCiudadesUpdated(ciudades);

            return ciudades;
        }

        partial void OnCiudadesDeleted(QCRM.Models.DB_157005_crm7des.Ciudades item);
        partial void OnAfterCiudadesDeleted(QCRM.Models.DB_157005_crm7des.Ciudades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ciudades> DeleteCiudades(string ciudad)
        {
            var itemToDelete = Context.Ciudades
                              .Where(i => i.CIUDAD == ciudad)
                              .Include(i => i.Contactos)
                              .Include(i => i.Cuentas)
                              .Include(i => i.Ejecutivos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCiudadesDeleted(itemToDelete);


            Context.Ciudades.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCiudadesDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportContactosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/contactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/contactos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportContactosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/contactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/contactos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnContactosRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Contactos> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Contactos>> GetContactos(Query query = null)
        {
            var items = Context.Contactos.AsQueryable();

            items = items.Include(i => i.Ciudades);
            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnContactosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnContactosGet(QCRM.Models.DB_157005_crm7des.Contactos item);
        partial void OnGetContactosByIdContacto(ref IQueryable<QCRM.Models.DB_157005_crm7des.Contactos> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Contactos> GetContactosByIdContacto(int idcontacto)
        {
            var items = Context.Contactos
                              .AsNoTracking()
                              .Where(i => i.ID_CONTACTO == idcontacto);

            items = items.Include(i => i.Ciudades);
            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Usuarios);
 
            OnGetContactosByIdContacto(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnContactosGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnContactosCreated(QCRM.Models.DB_157005_crm7des.Contactos item);
        partial void OnAfterContactosCreated(QCRM.Models.DB_157005_crm7des.Contactos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Contactos> CreateContactos(QCRM.Models.DB_157005_crm7des.Contactos contactos)
        {
            OnContactosCreated(contactos);

            var existingItem = Context.Contactos
                              .Where(i => i.ID_CONTACTO == contactos.ID_CONTACTO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Contactos.Add(contactos);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(contactos).State = EntityState.Detached;
                throw;
            }

            OnAfterContactosCreated(contactos);

            return contactos;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Contactos> CancelContactosChanges(QCRM.Models.DB_157005_crm7des.Contactos item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnContactosUpdated(QCRM.Models.DB_157005_crm7des.Contactos item);
        partial void OnAfterContactosUpdated(QCRM.Models.DB_157005_crm7des.Contactos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Contactos> UpdateContactos(int idcontacto, QCRM.Models.DB_157005_crm7des.Contactos contactos)
        {
            OnContactosUpdated(contactos);

            var itemToUpdate = Context.Contactos
                              .Where(i => i.ID_CONTACTO == contactos.ID_CONTACTO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(contactos);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterContactosUpdated(contactos);

            return contactos;
        }

        partial void OnContactosDeleted(QCRM.Models.DB_157005_crm7des.Contactos item);
        partial void OnAfterContactosDeleted(QCRM.Models.DB_157005_crm7des.Contactos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Contactos> DeleteContactos(int idcontacto)
        {
            var itemToDelete = Context.Contactos
                              .Where(i => i.ID_CONTACTO == idcontacto)
                              .Include(i => i.Oportunidades)
                              .Include(i => i.Oportunidades1)
                              .Include(i => i.Oportunidades2)
                              .Include(i => i.Oportunidades3)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnContactosDeleted(itemToDelete);


            Context.Contactos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterContactosDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCtalogToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/ctalog/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/ctalog/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCtalogToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/ctalog/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/ctalog/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCtalogRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Ctalog> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Ctalog>> GetCtalog(Query query = null)
        {
            var items = Context.Ctalog.AsQueryable();

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCtalogRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCtalogGet(QCRM.Models.DB_157005_crm7des.Ctalog item);
        partial void OnGetCtalogByIdCtalog(ref IQueryable<QCRM.Models.DB_157005_crm7des.Ctalog> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Ctalog> GetCtalogByIdCtalog(int idctalog)
        {
            var items = Context.Ctalog
                              .AsNoTracking()
                              .Where(i => i.ID_CTALOG == idctalog);

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Usuarios);
 
            OnGetCtalogByIdCtalog(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCtalogGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCtalogCreated(QCRM.Models.DB_157005_crm7des.Ctalog item);
        partial void OnAfterCtalogCreated(QCRM.Models.DB_157005_crm7des.Ctalog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ctalog> CreateCtalog(QCRM.Models.DB_157005_crm7des.Ctalog ctalog)
        {
            OnCtalogCreated(ctalog);

            var existingItem = Context.Ctalog
                              .Where(i => i.ID_CTALOG == ctalog.ID_CTALOG)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Ctalog.Add(ctalog);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(ctalog).State = EntityState.Detached;
                throw;
            }

            OnAfterCtalogCreated(ctalog);

            return ctalog;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Ctalog> CancelCtalogChanges(QCRM.Models.DB_157005_crm7des.Ctalog item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCtalogUpdated(QCRM.Models.DB_157005_crm7des.Ctalog item);
        partial void OnAfterCtalogUpdated(QCRM.Models.DB_157005_crm7des.Ctalog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ctalog> UpdateCtalog(int idctalog, QCRM.Models.DB_157005_crm7des.Ctalog ctalog)
        {
            OnCtalogUpdated(ctalog);

            var itemToUpdate = Context.Ctalog
                              .Where(i => i.ID_CTALOG == ctalog.ID_CTALOG)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(ctalog);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCtalogUpdated(ctalog);

            return ctalog;
        }

        partial void OnCtalogDeleted(QCRM.Models.DB_157005_crm7des.Ctalog item);
        partial void OnAfterCtalogDeleted(QCRM.Models.DB_157005_crm7des.Ctalog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ctalog> DeleteCtalog(int idctalog)
        {
            var itemToDelete = Context.Ctalog
                              .Where(i => i.ID_CTALOG == idctalog)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCtalogDeleted(itemToDelete);


            Context.Ctalog.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCtalogDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCuentasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/cuentas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/cuentas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCuentasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/cuentas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/cuentas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCuentasRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Cuentas> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Cuentas>> GetCuentas(Query query = null)
        {
            var items = Context.Cuentas.AsQueryable();

            items = items.Include(i => i.Ciudades);
            items = items.Include(i => i.Estados);
            items = items.Include(i => i.Grupos);
            items = items.Include(i => i.Industrias);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCuentasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCuentasGet(QCRM.Models.DB_157005_crm7des.Cuentas item);
        partial void OnGetCuentasByIdCuenta(ref IQueryable<QCRM.Models.DB_157005_crm7des.Cuentas> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Cuentas> GetCuentasByIdCuenta(int idcuenta)
        {
            var items = Context.Cuentas
                              .AsNoTracking()
                              .Where(i => i.ID_CUENTA == idcuenta);

            items = items.Include(i => i.Ciudades);
            items = items.Include(i => i.Estados);
            items = items.Include(i => i.Grupos);
            items = items.Include(i => i.Industrias);
            items = items.Include(i => i.Usuarios);
 
            OnGetCuentasByIdCuenta(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCuentasGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCuentasCreated(QCRM.Models.DB_157005_crm7des.Cuentas item);
        partial void OnAfterCuentasCreated(QCRM.Models.DB_157005_crm7des.Cuentas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cuentas> CreateCuentas(QCRM.Models.DB_157005_crm7des.Cuentas cuentas)
        {
            OnCuentasCreated(cuentas);

            var existingItem = Context.Cuentas
                              .Where(i => i.ID_CUENTA == cuentas.ID_CUENTA)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Cuentas.Add(cuentas);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cuentas).State = EntityState.Detached;
                throw;
            }

            OnAfterCuentasCreated(cuentas);

            return cuentas;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Cuentas> CancelCuentasChanges(QCRM.Models.DB_157005_crm7des.Cuentas item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCuentasUpdated(QCRM.Models.DB_157005_crm7des.Cuentas item);
        partial void OnAfterCuentasUpdated(QCRM.Models.DB_157005_crm7des.Cuentas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cuentas> UpdateCuentas(int idcuenta, QCRM.Models.DB_157005_crm7des.Cuentas cuentas)
        {
            OnCuentasUpdated(cuentas);

            var itemToUpdate = Context.Cuentas
                              .Where(i => i.ID_CUENTA == cuentas.ID_CUENTA)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cuentas);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCuentasUpdated(cuentas);

            return cuentas;
        }

        partial void OnCuentasDeleted(QCRM.Models.DB_157005_crm7des.Cuentas item);
        partial void OnAfterCuentasDeleted(QCRM.Models.DB_157005_crm7des.Cuentas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cuentas> DeleteCuentas(int idcuenta)
        {
            var itemToDelete = Context.Cuentas
                              .Where(i => i.ID_CUENTA == idcuenta)
                              .Include(i => i.Actividades)
                              .Include(i => i.Contactos)
                              .Include(i => i.Ctalog)
                              .Include(i => i.CuentaS5)
                              .Include(i => i.Documentos)
                              .Include(i => i.Ejecutivoscta)
                              .Include(i => i.Facturas)
                              .Include(i => i.Notascta)
                              .Include(i => i.Oportunidades)
                              .Include(i => i.Productosinst)
                              .Include(i => i.Proyectos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCuentasDeleted(itemToDelete);


            Context.Cuentas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCuentasDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCuentaS5ToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/cuentas5/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/cuentas5/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCuentaS5ToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/cuentas5/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/cuentas5/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCuentaS5Read(ref IQueryable<QCRM.Models.DB_157005_crm7des.CuentaS5> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.CuentaS5>> GetCuentaS5(Query query = null)
        {
            var items = Context.CuentaS5.AsQueryable();

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCuentaS5Read(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCuentaS5Get(QCRM.Models.DB_157005_crm7des.CuentaS5 item);
        partial void OnGetCuentaS5ById(ref IQueryable<QCRM.Models.DB_157005_crm7des.CuentaS5> items);


        public async Task<QCRM.Models.DB_157005_crm7des.CuentaS5> GetCuentaS5ById(int id)
        {
            var items = Context.CuentaS5
                              .AsNoTracking()
                              .Where(i => i.ID == id);

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Usuarios);
 
            OnGetCuentaS5ById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCuentaS5Get(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCuentaS5Created(QCRM.Models.DB_157005_crm7des.CuentaS5 item);
        partial void OnAfterCuentaS5Created(QCRM.Models.DB_157005_crm7des.CuentaS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.CuentaS5> CreateCuentaS5(QCRM.Models.DB_157005_crm7des.CuentaS5 cuentas5)
        {
            OnCuentaS5Created(cuentas5);

            var existingItem = Context.CuentaS5
                              .Where(i => i.ID == cuentas5.ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.CuentaS5.Add(cuentas5);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cuentas5).State = EntityState.Detached;
                throw;
            }

            OnAfterCuentaS5Created(cuentas5);

            return cuentas5;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.CuentaS5> CancelCuentaS5Changes(QCRM.Models.DB_157005_crm7des.CuentaS5 item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCuentaS5Updated(QCRM.Models.DB_157005_crm7des.CuentaS5 item);
        partial void OnAfterCuentaS5Updated(QCRM.Models.DB_157005_crm7des.CuentaS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.CuentaS5> UpdateCuentaS5(int id, QCRM.Models.DB_157005_crm7des.CuentaS5 cuentas5)
        {
            OnCuentaS5Updated(cuentas5);

            var itemToUpdate = Context.CuentaS5
                              .Where(i => i.ID == cuentas5.ID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cuentas5);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCuentaS5Updated(cuentas5);

            return cuentas5;
        }

        partial void OnCuentaS5Deleted(QCRM.Models.DB_157005_crm7des.CuentaS5 item);
        partial void OnAfterCuentaS5Deleted(QCRM.Models.DB_157005_crm7des.CuentaS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.CuentaS5> DeleteCuentaS5(int id)
        {
            var itemToDelete = Context.CuentaS5
                              .Where(i => i.ID == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCuentaS5Deleted(itemToDelete);


            Context.CuentaS5.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCuentaS5Deleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportCuotasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/cuotas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/cuotas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportCuotasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/cuotas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/cuotas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnCuotasRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Cuotas> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Cuotas>> GetCuotas(Query query = null)
        {
            var items = Context.Cuotas.AsQueryable();

            items = items.Include(i => i.Tiposerv);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnCuotasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnCuotasGet(QCRM.Models.DB_157005_crm7des.Cuotas item);
        partial void OnGetCuotasById(ref IQueryable<QCRM.Models.DB_157005_crm7des.Cuotas> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Cuotas> GetCuotasById(int id)
        {
            var items = Context.Cuotas
                              .AsNoTracking()
                              .Where(i => i.ID == id);

            items = items.Include(i => i.Tiposerv);
 
            OnGetCuotasById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnCuotasGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnCuotasCreated(QCRM.Models.DB_157005_crm7des.Cuotas item);
        partial void OnAfterCuotasCreated(QCRM.Models.DB_157005_crm7des.Cuotas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cuotas> CreateCuotas(QCRM.Models.DB_157005_crm7des.Cuotas cuotas)
        {
            OnCuotasCreated(cuotas);

            var existingItem = Context.Cuotas
                              .Where(i => i.ID == cuotas.ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Cuotas.Add(cuotas);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(cuotas).State = EntityState.Detached;
                throw;
            }

            OnAfterCuotasCreated(cuotas);

            return cuotas;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Cuotas> CancelCuotasChanges(QCRM.Models.DB_157005_crm7des.Cuotas item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnCuotasUpdated(QCRM.Models.DB_157005_crm7des.Cuotas item);
        partial void OnAfterCuotasUpdated(QCRM.Models.DB_157005_crm7des.Cuotas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cuotas> UpdateCuotas(int id, QCRM.Models.DB_157005_crm7des.Cuotas cuotas)
        {
            OnCuotasUpdated(cuotas);

            var itemToUpdate = Context.Cuotas
                              .Where(i => i.ID == cuotas.ID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(cuotas);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterCuotasUpdated(cuotas);

            return cuotas;
        }

        partial void OnCuotasDeleted(QCRM.Models.DB_157005_crm7des.Cuotas item);
        partial void OnAfterCuotasDeleted(QCRM.Models.DB_157005_crm7des.Cuotas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Cuotas> DeleteCuotas(int id)
        {
            var itemToDelete = Context.Cuotas
                              .Where(i => i.ID == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnCuotasDeleted(itemToDelete);


            Context.Cuotas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterCuotasDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportDocumentosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/documentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/documentos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportDocumentosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/documentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/documentos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnDocumentosRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Documentos> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Documentos>> GetDocumentos(Query query = null)
        {
            var items = Context.Documentos.AsQueryable();

            items = items.Include(i => i.Actividades);
            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Tiposdoc);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnDocumentosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnDocumentosGet(QCRM.Models.DB_157005_crm7des.Documentos item);
        partial void OnGetDocumentosByIdDoc(ref IQueryable<QCRM.Models.DB_157005_crm7des.Documentos> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Documentos> GetDocumentosByIdDoc(int iddoc)
        {
            var items = Context.Documentos
                              .AsNoTracking()
                              .Where(i => i.ID_DOC == iddoc);

            items = items.Include(i => i.Actividades);
            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Tiposdoc);
            items = items.Include(i => i.Usuarios);
 
            OnGetDocumentosByIdDoc(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnDocumentosGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnDocumentosCreated(QCRM.Models.DB_157005_crm7des.Documentos item);
        partial void OnAfterDocumentosCreated(QCRM.Models.DB_157005_crm7des.Documentos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Documentos> CreateDocumentos(QCRM.Models.DB_157005_crm7des.Documentos documentos)
        {
            OnDocumentosCreated(documentos);

            var existingItem = Context.Documentos
                              .Where(i => i.ID_DOC == documentos.ID_DOC)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Documentos.Add(documentos);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(documentos).State = EntityState.Detached;
                throw;
            }

            OnAfterDocumentosCreated(documentos);

            return documentos;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Documentos> CancelDocumentosChanges(QCRM.Models.DB_157005_crm7des.Documentos item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnDocumentosUpdated(QCRM.Models.DB_157005_crm7des.Documentos item);
        partial void OnAfterDocumentosUpdated(QCRM.Models.DB_157005_crm7des.Documentos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Documentos> UpdateDocumentos(int iddoc, QCRM.Models.DB_157005_crm7des.Documentos documentos)
        {
            OnDocumentosUpdated(documentos);

            var itemToUpdate = Context.Documentos
                              .Where(i => i.ID_DOC == documentos.ID_DOC)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(documentos);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterDocumentosUpdated(documentos);

            return documentos;
        }

        partial void OnDocumentosDeleted(QCRM.Models.DB_157005_crm7des.Documentos item);
        partial void OnAfterDocumentosDeleted(QCRM.Models.DB_157005_crm7des.Documentos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Documentos> DeleteDocumentos(int iddoc)
        {
            var itemToDelete = Context.Documentos
                              .Where(i => i.ID_DOC == iddoc)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnDocumentosDeleted(itemToDelete);


            Context.Documentos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterDocumentosDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEjecutivosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/ejecutivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/ejecutivos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEjecutivosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/ejecutivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/ejecutivos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEjecutivosRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Ejecutivos> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Ejecutivos>> GetEjecutivos(Query query = null)
        {
            var items = Context.Ejecutivos.AsQueryable();

            items = items.Include(i => i.Ciudades);
            items = items.Include(i => i.Fabricantes);
            items = items.Include(i => i.Usuarios);
            items = items.Include(i => i.Verticales);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEjecutivosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEjecutivosGet(QCRM.Models.DB_157005_crm7des.Ejecutivos item);
        partial void OnGetEjecutivosByIdEjec(ref IQueryable<QCRM.Models.DB_157005_crm7des.Ejecutivos> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivos> GetEjecutivosByIdEjec(int idejec)
        {
            var items = Context.Ejecutivos
                              .AsNoTracking()
                              .Where(i => i.ID_EJEC == idejec);

            items = items.Include(i => i.Ciudades);
            items = items.Include(i => i.Fabricantes);
            items = items.Include(i => i.Usuarios);
            items = items.Include(i => i.Verticales);
 
            OnGetEjecutivosByIdEjec(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEjecutivosGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEjecutivosCreated(QCRM.Models.DB_157005_crm7des.Ejecutivos item);
        partial void OnAfterEjecutivosCreated(QCRM.Models.DB_157005_crm7des.Ejecutivos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivos> CreateEjecutivos(QCRM.Models.DB_157005_crm7des.Ejecutivos ejecutivos)
        {
            OnEjecutivosCreated(ejecutivos);

            var existingItem = Context.Ejecutivos
                              .Where(i => i.ID_EJEC == ejecutivos.ID_EJEC)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Ejecutivos.Add(ejecutivos);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(ejecutivos).State = EntityState.Detached;
                throw;
            }

            OnAfterEjecutivosCreated(ejecutivos);

            return ejecutivos;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivos> CancelEjecutivosChanges(QCRM.Models.DB_157005_crm7des.Ejecutivos item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEjecutivosUpdated(QCRM.Models.DB_157005_crm7des.Ejecutivos item);
        partial void OnAfterEjecutivosUpdated(QCRM.Models.DB_157005_crm7des.Ejecutivos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivos> UpdateEjecutivos(int idejec, QCRM.Models.DB_157005_crm7des.Ejecutivos ejecutivos)
        {
            OnEjecutivosUpdated(ejecutivos);

            var itemToUpdate = Context.Ejecutivos
                              .Where(i => i.ID_EJEC == ejecutivos.ID_EJEC)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(ejecutivos);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEjecutivosUpdated(ejecutivos);

            return ejecutivos;
        }

        partial void OnEjecutivosDeleted(QCRM.Models.DB_157005_crm7des.Ejecutivos item);
        partial void OnAfterEjecutivosDeleted(QCRM.Models.DB_157005_crm7des.Ejecutivos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivos> DeleteEjecutivos(int idejec)
        {
            var itemToDelete = Context.Ejecutivos
                              .Where(i => i.ID_EJEC == idejec)
                              .Include(i => i.Oportunidades)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEjecutivosDeleted(itemToDelete);


            Context.Ejecutivos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEjecutivosDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEjecutivosctaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/ejecutivoscta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/ejecutivoscta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEjecutivosctaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/ejecutivoscta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/ejecutivoscta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEjecutivosctaRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Ejecutivoscta> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Ejecutivoscta>> GetEjecutivoscta(Query query = null)
        {
            var items = Context.Ejecutivoscta.AsQueryable();

            items = items.Include(i => i.Fabricantes);
            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Verticales);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEjecutivosctaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEjecutivosctaGet(QCRM.Models.DB_157005_crm7des.Ejecutivoscta item);
        partial void OnGetEjecutivosctaByIdEjecutivoCuenta(ref IQueryable<QCRM.Models.DB_157005_crm7des.Ejecutivoscta> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivoscta> GetEjecutivosctaByIdEjecutivoCuenta(int idejecutivocuenta)
        {
            var items = Context.Ejecutivoscta
                              .AsNoTracking()
                              .Where(i => i.ID_EJECUTIVO_CUENTA == idejecutivocuenta);

            items = items.Include(i => i.Fabricantes);
            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Verticales);
 
            OnGetEjecutivosctaByIdEjecutivoCuenta(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEjecutivosctaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEjecutivosctaCreated(QCRM.Models.DB_157005_crm7des.Ejecutivoscta item);
        partial void OnAfterEjecutivosctaCreated(QCRM.Models.DB_157005_crm7des.Ejecutivoscta item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivoscta> CreateEjecutivoscta(QCRM.Models.DB_157005_crm7des.Ejecutivoscta ejecutivoscta)
        {
            OnEjecutivosctaCreated(ejecutivoscta);

            var existingItem = Context.Ejecutivoscta
                              .Where(i => i.ID_EJECUTIVO_CUENTA == ejecutivoscta.ID_EJECUTIVO_CUENTA)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Ejecutivoscta.Add(ejecutivoscta);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(ejecutivoscta).State = EntityState.Detached;
                throw;
            }

            OnAfterEjecutivosctaCreated(ejecutivoscta);

            return ejecutivoscta;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivoscta> CancelEjecutivosctaChanges(QCRM.Models.DB_157005_crm7des.Ejecutivoscta item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEjecutivosctaUpdated(QCRM.Models.DB_157005_crm7des.Ejecutivoscta item);
        partial void OnAfterEjecutivosctaUpdated(QCRM.Models.DB_157005_crm7des.Ejecutivoscta item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivoscta> UpdateEjecutivoscta(int idejecutivocuenta, QCRM.Models.DB_157005_crm7des.Ejecutivoscta ejecutivoscta)
        {
            OnEjecutivosctaUpdated(ejecutivoscta);

            var itemToUpdate = Context.Ejecutivoscta
                              .Where(i => i.ID_EJECUTIVO_CUENTA == ejecutivoscta.ID_EJECUTIVO_CUENTA)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(ejecutivoscta);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEjecutivosctaUpdated(ejecutivoscta);

            return ejecutivoscta;
        }

        partial void OnEjecutivosctaDeleted(QCRM.Models.DB_157005_crm7des.Ejecutivoscta item);
        partial void OnAfterEjecutivosctaDeleted(QCRM.Models.DB_157005_crm7des.Ejecutivoscta item);

        public async Task<QCRM.Models.DB_157005_crm7des.Ejecutivoscta> DeleteEjecutivoscta(int idejecutivocuenta)
        {
            var itemToDelete = Context.Ejecutivoscta
                              .Where(i => i.ID_EJECUTIVO_CUENTA == idejecutivocuenta)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEjecutivosctaDeleted(itemToDelete);


            Context.Ejecutivoscta.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEjecutivosctaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEstadosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/estados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/estados/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEstadosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/estados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/estados/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEstadosRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Estados> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Estados>> GetEstados(Query query = null)
        {
            var items = Context.Estados.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEstadosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEstadosGet(QCRM.Models.DB_157005_crm7des.Estados item);
        partial void OnGetEstadosByEstado(ref IQueryable<QCRM.Models.DB_157005_crm7des.Estados> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Estados> GetEstadosByEstado(string estado)
        {
            var items = Context.Estados
                              .AsNoTracking()
                              .Where(i => i.ESTADO == estado);

 
            OnGetEstadosByEstado(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEstadosGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEstadosCreated(QCRM.Models.DB_157005_crm7des.Estados item);
        partial void OnAfterEstadosCreated(QCRM.Models.DB_157005_crm7des.Estados item);

        public async Task<QCRM.Models.DB_157005_crm7des.Estados> CreateEstados(QCRM.Models.DB_157005_crm7des.Estados estados)
        {
            OnEstadosCreated(estados);

            var existingItem = Context.Estados
                              .Where(i => i.ESTADO == estados.ESTADO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Estados.Add(estados);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(estados).State = EntityState.Detached;
                throw;
            }

            OnAfterEstadosCreated(estados);

            return estados;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Estados> CancelEstadosChanges(QCRM.Models.DB_157005_crm7des.Estados item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEstadosUpdated(QCRM.Models.DB_157005_crm7des.Estados item);
        partial void OnAfterEstadosUpdated(QCRM.Models.DB_157005_crm7des.Estados item);

        public async Task<QCRM.Models.DB_157005_crm7des.Estados> UpdateEstados(string estado, QCRM.Models.DB_157005_crm7des.Estados estados)
        {
            OnEstadosUpdated(estados);

            var itemToUpdate = Context.Estados
                              .Where(i => i.ESTADO == estados.ESTADO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(estados);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEstadosUpdated(estados);

            return estados;
        }

        partial void OnEstadosDeleted(QCRM.Models.DB_157005_crm7des.Estados item);
        partial void OnAfterEstadosDeleted(QCRM.Models.DB_157005_crm7des.Estados item);

        public async Task<QCRM.Models.DB_157005_crm7des.Estados> DeleteEstados(string estado)
        {
            var itemToDelete = Context.Estados
                              .Where(i => i.ESTADO == estado)
                              .Include(i => i.Cuentas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEstadosDeleted(itemToDelete);


            Context.Estados.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEstadosDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportEtapasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/etapas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/etapas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportEtapasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/etapas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/etapas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnEtapasRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Etapas> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Etapas>> GetEtapas(Query query = null)
        {
            var items = Context.Etapas.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnEtapasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnEtapasGet(QCRM.Models.DB_157005_crm7des.Etapas item);
        partial void OnGetEtapasByEtapa(ref IQueryable<QCRM.Models.DB_157005_crm7des.Etapas> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Etapas> GetEtapasByEtapa(string etapa)
        {
            var items = Context.Etapas
                              .AsNoTracking()
                              .Where(i => i.ETAPA == etapa);

 
            OnGetEtapasByEtapa(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnEtapasGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnEtapasCreated(QCRM.Models.DB_157005_crm7des.Etapas item);
        partial void OnAfterEtapasCreated(QCRM.Models.DB_157005_crm7des.Etapas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Etapas> CreateEtapas(QCRM.Models.DB_157005_crm7des.Etapas etapas)
        {
            OnEtapasCreated(etapas);

            var existingItem = Context.Etapas
                              .Where(i => i.ETAPA == etapas.ETAPA)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Etapas.Add(etapas);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(etapas).State = EntityState.Detached;
                throw;
            }

            OnAfterEtapasCreated(etapas);

            return etapas;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Etapas> CancelEtapasChanges(QCRM.Models.DB_157005_crm7des.Etapas item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnEtapasUpdated(QCRM.Models.DB_157005_crm7des.Etapas item);
        partial void OnAfterEtapasUpdated(QCRM.Models.DB_157005_crm7des.Etapas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Etapas> UpdateEtapas(string etapa, QCRM.Models.DB_157005_crm7des.Etapas etapas)
        {
            OnEtapasUpdated(etapas);

            var itemToUpdate = Context.Etapas
                              .Where(i => i.ETAPA == etapas.ETAPA)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(etapas);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterEtapasUpdated(etapas);

            return etapas;
        }

        partial void OnEtapasDeleted(QCRM.Models.DB_157005_crm7des.Etapas item);
        partial void OnAfterEtapasDeleted(QCRM.Models.DB_157005_crm7des.Etapas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Etapas> DeleteEtapas(string etapa)
        {
            var itemToDelete = Context.Etapas
                              .Where(i => i.ETAPA == etapa)
                              .Include(i => i.Oportunidades)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnEtapasDeleted(itemToDelete);


            Context.Etapas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterEtapasDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportFabricantesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/fabricantes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/fabricantes/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFabricantesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/fabricantes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/fabricantes/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFabricantesRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Fabricantes> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Fabricantes>> GetFabricantes(Query query = null)
        {
            var items = Context.Fabricantes.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnFabricantesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFabricantesGet(QCRM.Models.DB_157005_crm7des.Fabricantes item);
        partial void OnGetFabricantesByFabricante(ref IQueryable<QCRM.Models.DB_157005_crm7des.Fabricantes> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Fabricantes> GetFabricantesByFabricante(string fabricante)
        {
            var items = Context.Fabricantes
                              .AsNoTracking()
                              .Where(i => i.FABRICANTE == fabricante);

 
            OnGetFabricantesByFabricante(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnFabricantesGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFabricantesCreated(QCRM.Models.DB_157005_crm7des.Fabricantes item);
        partial void OnAfterFabricantesCreated(QCRM.Models.DB_157005_crm7des.Fabricantes item);

        public async Task<QCRM.Models.DB_157005_crm7des.Fabricantes> CreateFabricantes(QCRM.Models.DB_157005_crm7des.Fabricantes fabricantes)
        {
            OnFabricantesCreated(fabricantes);

            var existingItem = Context.Fabricantes
                              .Where(i => i.FABRICANTE == fabricantes.FABRICANTE)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Fabricantes.Add(fabricantes);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(fabricantes).State = EntityState.Detached;
                throw;
            }

            OnAfterFabricantesCreated(fabricantes);

            return fabricantes;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Fabricantes> CancelFabricantesChanges(QCRM.Models.DB_157005_crm7des.Fabricantes item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFabricantesUpdated(QCRM.Models.DB_157005_crm7des.Fabricantes item);
        partial void OnAfterFabricantesUpdated(QCRM.Models.DB_157005_crm7des.Fabricantes item);

        public async Task<QCRM.Models.DB_157005_crm7des.Fabricantes> UpdateFabricantes(string fabricante, QCRM.Models.DB_157005_crm7des.Fabricantes fabricantes)
        {
            OnFabricantesUpdated(fabricantes);

            var itemToUpdate = Context.Fabricantes
                              .Where(i => i.FABRICANTE == fabricantes.FABRICANTE)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(fabricantes);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFabricantesUpdated(fabricantes);

            return fabricantes;
        }

        partial void OnFabricantesDeleted(QCRM.Models.DB_157005_crm7des.Fabricantes item);
        partial void OnAfterFabricantesDeleted(QCRM.Models.DB_157005_crm7des.Fabricantes item);

        public async Task<QCRM.Models.DB_157005_crm7des.Fabricantes> DeleteFabricantes(string fabricante)
        {
            var itemToDelete = Context.Fabricantes
                              .Where(i => i.FABRICANTE == fabricante)
                              .Include(i => i.Ejecutivos)
                              .Include(i => i.Ejecutivoscta)
                              .Include(i => i.Oportunidades)
                              .Include(i => i.Productosinst)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFabricantesDeleted(itemToDelete);


            Context.Fabricantes.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFabricantesDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportFacturasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/facturas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/facturas/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFacturasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/facturas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/facturas/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFacturasRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Facturas> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Facturas>> GetFacturas(Query query = null)
        {
            var items = Context.Facturas.AsQueryable();

            items = items.Include(i => i.Cuentas);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnFacturasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFacturasGet(QCRM.Models.DB_157005_crm7des.Facturas item);
        partial void OnGetFacturasByIdFactura(ref IQueryable<QCRM.Models.DB_157005_crm7des.Facturas> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Facturas> GetFacturasByIdFactura(string idfactura)
        {
            var items = Context.Facturas
                              .AsNoTracking()
                              .Where(i => i.ID_FACTURA == idfactura);

            items = items.Include(i => i.Cuentas);
 
            OnGetFacturasByIdFactura(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnFacturasGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFacturasCreated(QCRM.Models.DB_157005_crm7des.Facturas item);
        partial void OnAfterFacturasCreated(QCRM.Models.DB_157005_crm7des.Facturas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Facturas> CreateFacturas(QCRM.Models.DB_157005_crm7des.Facturas facturas)
        {
            OnFacturasCreated(facturas);

            var existingItem = Context.Facturas
                              .Where(i => i.ID_FACTURA == facturas.ID_FACTURA)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Facturas.Add(facturas);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(facturas).State = EntityState.Detached;
                throw;
            }

            OnAfterFacturasCreated(facturas);

            return facturas;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Facturas> CancelFacturasChanges(QCRM.Models.DB_157005_crm7des.Facturas item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFacturasUpdated(QCRM.Models.DB_157005_crm7des.Facturas item);
        partial void OnAfterFacturasUpdated(QCRM.Models.DB_157005_crm7des.Facturas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Facturas> UpdateFacturas(string idfactura, QCRM.Models.DB_157005_crm7des.Facturas facturas)
        {
            OnFacturasUpdated(facturas);

            var itemToUpdate = Context.Facturas
                              .Where(i => i.ID_FACTURA == facturas.ID_FACTURA)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(facturas);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFacturasUpdated(facturas);

            return facturas;
        }

        partial void OnFacturasDeleted(QCRM.Models.DB_157005_crm7des.Facturas item);
        partial void OnAfterFacturasDeleted(QCRM.Models.DB_157005_crm7des.Facturas item);

        public async Task<QCRM.Models.DB_157005_crm7des.Facturas> DeleteFacturas(string idfactura)
        {
            var itemToDelete = Context.Facturas
                              .Where(i => i.ID_FACTURA == idfactura)
                              .Include(i => i.Facturasl)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFacturasDeleted(itemToDelete);


            Context.Facturas.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFacturasDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportFacturaslToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/facturasl/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/facturasl/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportFacturaslToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/facturasl/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/facturasl/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnFacturaslRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Facturasl> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Facturasl>> GetFacturasl(Query query = null)
        {
            var items = Context.Facturasl.AsQueryable();

            items = items.Include(i => i.Facturas);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnFacturaslRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnFacturaslGet(QCRM.Models.DB_157005_crm7des.Facturasl item);
        partial void OnGetFacturaslByIdLinea(ref IQueryable<QCRM.Models.DB_157005_crm7des.Facturasl> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Facturasl> GetFacturaslByIdLinea(int idlinea)
        {
            var items = Context.Facturasl
                              .AsNoTracking()
                              .Where(i => i.ID_LINEA == idlinea);

            items = items.Include(i => i.Facturas);
 
            OnGetFacturaslByIdLinea(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnFacturaslGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnFacturaslCreated(QCRM.Models.DB_157005_crm7des.Facturasl item);
        partial void OnAfterFacturaslCreated(QCRM.Models.DB_157005_crm7des.Facturasl item);

        public async Task<QCRM.Models.DB_157005_crm7des.Facturasl> CreateFacturasl(QCRM.Models.DB_157005_crm7des.Facturasl facturasl)
        {
            OnFacturaslCreated(facturasl);

            var existingItem = Context.Facturasl
                              .Where(i => i.ID_LINEA == facturasl.ID_LINEA)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Facturasl.Add(facturasl);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(facturasl).State = EntityState.Detached;
                throw;
            }

            OnAfterFacturaslCreated(facturasl);

            return facturasl;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Facturasl> CancelFacturaslChanges(QCRM.Models.DB_157005_crm7des.Facturasl item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnFacturaslUpdated(QCRM.Models.DB_157005_crm7des.Facturasl item);
        partial void OnAfterFacturaslUpdated(QCRM.Models.DB_157005_crm7des.Facturasl item);

        public async Task<QCRM.Models.DB_157005_crm7des.Facturasl> UpdateFacturasl(int idlinea, QCRM.Models.DB_157005_crm7des.Facturasl facturasl)
        {
            OnFacturaslUpdated(facturasl);

            var itemToUpdate = Context.Facturasl
                              .Where(i => i.ID_LINEA == facturasl.ID_LINEA)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(facturasl);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterFacturaslUpdated(facturasl);

            return facturasl;
        }

        partial void OnFacturaslDeleted(QCRM.Models.DB_157005_crm7des.Facturasl item);
        partial void OnAfterFacturaslDeleted(QCRM.Models.DB_157005_crm7des.Facturasl item);

        public async Task<QCRM.Models.DB_157005_crm7des.Facturasl> DeleteFacturasl(int idlinea)
        {
            var itemToDelete = Context.Facturasl
                              .Where(i => i.ID_LINEA == idlinea)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnFacturaslDeleted(itemToDelete);


            Context.Facturasl.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterFacturaslDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportGruposToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/grupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/grupos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportGruposToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/grupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/grupos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnGruposRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Grupos> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Grupos>> GetGrupos(Query query = null)
        {
            var items = Context.Grupos.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnGruposRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnGruposGet(QCRM.Models.DB_157005_crm7des.Grupos item);
        partial void OnGetGruposByGrupo(ref IQueryable<QCRM.Models.DB_157005_crm7des.Grupos> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Grupos> GetGruposByGrupo(string grupo)
        {
            var items = Context.Grupos
                              .AsNoTracking()
                              .Where(i => i.GRUPO == grupo);

 
            OnGetGruposByGrupo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnGruposGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnGruposCreated(QCRM.Models.DB_157005_crm7des.Grupos item);
        partial void OnAfterGruposCreated(QCRM.Models.DB_157005_crm7des.Grupos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Grupos> CreateGrupos(QCRM.Models.DB_157005_crm7des.Grupos grupos)
        {
            OnGruposCreated(grupos);

            var existingItem = Context.Grupos
                              .Where(i => i.GRUPO == grupos.GRUPO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Grupos.Add(grupos);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(grupos).State = EntityState.Detached;
                throw;
            }

            OnAfterGruposCreated(grupos);

            return grupos;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Grupos> CancelGruposChanges(QCRM.Models.DB_157005_crm7des.Grupos item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnGruposUpdated(QCRM.Models.DB_157005_crm7des.Grupos item);
        partial void OnAfterGruposUpdated(QCRM.Models.DB_157005_crm7des.Grupos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Grupos> UpdateGrupos(string grupo, QCRM.Models.DB_157005_crm7des.Grupos grupos)
        {
            OnGruposUpdated(grupos);

            var itemToUpdate = Context.Grupos
                              .Where(i => i.GRUPO == grupos.GRUPO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(grupos);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterGruposUpdated(grupos);

            return grupos;
        }

        partial void OnGruposDeleted(QCRM.Models.DB_157005_crm7des.Grupos item);
        partial void OnAfterGruposDeleted(QCRM.Models.DB_157005_crm7des.Grupos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Grupos> DeleteGrupos(string grupo)
        {
            var itemToDelete = Context.Grupos
                              .Where(i => i.GRUPO == grupo)
                              .Include(i => i.Cuentas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnGruposDeleted(itemToDelete);


            Context.Grupos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterGruposDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportIndustriasToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/industrias/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/industrias/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportIndustriasToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/industrias/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/industrias/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnIndustriasRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Industrias> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Industrias>> GetIndustrias(Query query = null)
        {
            var items = Context.Industrias.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnIndustriasRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnIndustriasGet(QCRM.Models.DB_157005_crm7des.Industrias item);
        partial void OnGetIndustriasByIndustria(ref IQueryable<QCRM.Models.DB_157005_crm7des.Industrias> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Industrias> GetIndustriasByIndustria(string industria)
        {
            var items = Context.Industrias
                              .AsNoTracking()
                              .Where(i => i.INDUSTRIA == industria);

 
            OnGetIndustriasByIndustria(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnIndustriasGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnIndustriasCreated(QCRM.Models.DB_157005_crm7des.Industrias item);
        partial void OnAfterIndustriasCreated(QCRM.Models.DB_157005_crm7des.Industrias item);

        public async Task<QCRM.Models.DB_157005_crm7des.Industrias> CreateIndustrias(QCRM.Models.DB_157005_crm7des.Industrias industrias)
        {
            OnIndustriasCreated(industrias);

            var existingItem = Context.Industrias
                              .Where(i => i.INDUSTRIA == industrias.INDUSTRIA)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Industrias.Add(industrias);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(industrias).State = EntityState.Detached;
                throw;
            }

            OnAfterIndustriasCreated(industrias);

            return industrias;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Industrias> CancelIndustriasChanges(QCRM.Models.DB_157005_crm7des.Industrias item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnIndustriasUpdated(QCRM.Models.DB_157005_crm7des.Industrias item);
        partial void OnAfterIndustriasUpdated(QCRM.Models.DB_157005_crm7des.Industrias item);

        public async Task<QCRM.Models.DB_157005_crm7des.Industrias> UpdateIndustrias(string industria, QCRM.Models.DB_157005_crm7des.Industrias industrias)
        {
            OnIndustriasUpdated(industrias);

            var itemToUpdate = Context.Industrias
                              .Where(i => i.INDUSTRIA == industrias.INDUSTRIA)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(industrias);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterIndustriasUpdated(industrias);

            return industrias;
        }

        partial void OnIndustriasDeleted(QCRM.Models.DB_157005_crm7des.Industrias item);
        partial void OnAfterIndustriasDeleted(QCRM.Models.DB_157005_crm7des.Industrias item);

        public async Task<QCRM.Models.DB_157005_crm7des.Industrias> DeleteIndustrias(string industria)
        {
            var itemToDelete = Context.Industrias
                              .Where(i => i.INDUSTRIA == industria)
                              .Include(i => i.Cuentas)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnIndustriasDeleted(itemToDelete);


            Context.Industrias.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterIndustriasDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportNotasctaToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/notascta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/notascta/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportNotasctaToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/notascta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/notascta/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnNotasctaRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Notascta> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Notascta>> GetNotascta(Query query = null)
        {
            var items = Context.Notascta.AsQueryable();

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnNotasctaRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnNotasctaGet(QCRM.Models.DB_157005_crm7des.Notascta item);
        partial void OnGetNotasctaById(ref IQueryable<QCRM.Models.DB_157005_crm7des.Notascta> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Notascta> GetNotasctaById(int id)
        {
            var items = Context.Notascta
                              .AsNoTracking()
                              .Where(i => i.ID == id);

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Usuarios);
 
            OnGetNotasctaById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnNotasctaGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnNotasctaCreated(QCRM.Models.DB_157005_crm7des.Notascta item);
        partial void OnAfterNotasctaCreated(QCRM.Models.DB_157005_crm7des.Notascta item);

        public async Task<QCRM.Models.DB_157005_crm7des.Notascta> CreateNotascta(QCRM.Models.DB_157005_crm7des.Notascta notascta)
        {
            OnNotasctaCreated(notascta);

            var existingItem = Context.Notascta
                              .Where(i => i.ID == notascta.ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Notascta.Add(notascta);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(notascta).State = EntityState.Detached;
                throw;
            }

            OnAfterNotasctaCreated(notascta);

            return notascta;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Notascta> CancelNotasctaChanges(QCRM.Models.DB_157005_crm7des.Notascta item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnNotasctaUpdated(QCRM.Models.DB_157005_crm7des.Notascta item);
        partial void OnAfterNotasctaUpdated(QCRM.Models.DB_157005_crm7des.Notascta item);

        public async Task<QCRM.Models.DB_157005_crm7des.Notascta> UpdateNotascta(int id, QCRM.Models.DB_157005_crm7des.Notascta notascta)
        {
            OnNotasctaUpdated(notascta);

            var itemToUpdate = Context.Notascta
                              .Where(i => i.ID == notascta.ID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(notascta);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterNotasctaUpdated(notascta);

            return notascta;
        }

        partial void OnNotasctaDeleted(QCRM.Models.DB_157005_crm7des.Notascta item);
        partial void OnAfterNotasctaDeleted(QCRM.Models.DB_157005_crm7des.Notascta item);

        public async Task<QCRM.Models.DB_157005_crm7des.Notascta> DeleteNotascta(int id)
        {
            var itemToDelete = Context.Notascta
                              .Where(i => i.ID == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnNotasctaDeleted(itemToDelete);


            Context.Notascta.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterNotasctaDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportNotiflogToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/notiflog/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/notiflog/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportNotiflogToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/notiflog/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/notiflog/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnNotiflogRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Notiflog> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Notiflog>> GetNotiflog(Query query = null)
        {
            var items = Context.Notiflog.AsQueryable();

            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnNotiflogRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnNotiflogGet(QCRM.Models.DB_157005_crm7des.Notiflog item);
        partial void OnGetNotiflogByIdNotif(ref IQueryable<QCRM.Models.DB_157005_crm7des.Notiflog> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Notiflog> GetNotiflogByIdNotif(int idnotif)
        {
            var items = Context.Notiflog
                              .AsNoTracking()
                              .Where(i => i.ID_NOTIF == idnotif);

            items = items.Include(i => i.Usuarios);
 
            OnGetNotiflogByIdNotif(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnNotiflogGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnNotiflogCreated(QCRM.Models.DB_157005_crm7des.Notiflog item);
        partial void OnAfterNotiflogCreated(QCRM.Models.DB_157005_crm7des.Notiflog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Notiflog> CreateNotiflog(QCRM.Models.DB_157005_crm7des.Notiflog notiflog)
        {
            OnNotiflogCreated(notiflog);

            var existingItem = Context.Notiflog
                              .Where(i => i.ID_NOTIF == notiflog.ID_NOTIF)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Notiflog.Add(notiflog);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(notiflog).State = EntityState.Detached;
                throw;
            }

            OnAfterNotiflogCreated(notiflog);

            return notiflog;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Notiflog> CancelNotiflogChanges(QCRM.Models.DB_157005_crm7des.Notiflog item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnNotiflogUpdated(QCRM.Models.DB_157005_crm7des.Notiflog item);
        partial void OnAfterNotiflogUpdated(QCRM.Models.DB_157005_crm7des.Notiflog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Notiflog> UpdateNotiflog(int idnotif, QCRM.Models.DB_157005_crm7des.Notiflog notiflog)
        {
            OnNotiflogUpdated(notiflog);

            var itemToUpdate = Context.Notiflog
                              .Where(i => i.ID_NOTIF == notiflog.ID_NOTIF)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(notiflog);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterNotiflogUpdated(notiflog);

            return notiflog;
        }

        partial void OnNotiflogDeleted(QCRM.Models.DB_157005_crm7des.Notiflog item);
        partial void OnAfterNotiflogDeleted(QCRM.Models.DB_157005_crm7des.Notiflog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Notiflog> DeleteNotiflog(int idnotif)
        {
            var itemToDelete = Context.Notiflog
                              .Where(i => i.ID_NOTIF == idnotif)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnNotiflogDeleted(itemToDelete);


            Context.Notiflog.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterNotiflogDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportOpologToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/opolog/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/opolog/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOpologToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/opolog/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/opolog/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOpologRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Opolog> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Opolog>> GetOpolog(Query query = null)
        {
            var items = Context.Opolog.AsQueryable();

            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnOpologRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOpologGet(QCRM.Models.DB_157005_crm7des.Opolog item);
        partial void OnGetOpologByIdOpolog(ref IQueryable<QCRM.Models.DB_157005_crm7des.Opolog> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Opolog> GetOpologByIdOpolog(int idopolog)
        {
            var items = Context.Opolog
                              .AsNoTracking()
                              .Where(i => i.ID_OPOLOG == idopolog);

            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Usuarios);
 
            OnGetOpologByIdOpolog(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnOpologGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnOpologCreated(QCRM.Models.DB_157005_crm7des.Opolog item);
        partial void OnAfterOpologCreated(QCRM.Models.DB_157005_crm7des.Opolog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Opolog> CreateOpolog(QCRM.Models.DB_157005_crm7des.Opolog opolog)
        {
            OnOpologCreated(opolog);

            var existingItem = Context.Opolog
                              .Where(i => i.ID_OPOLOG == opolog.ID_OPOLOG)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Opolog.Add(opolog);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(opolog).State = EntityState.Detached;
                throw;
            }

            OnAfterOpologCreated(opolog);

            return opolog;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Opolog> CancelOpologChanges(QCRM.Models.DB_157005_crm7des.Opolog item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnOpologUpdated(QCRM.Models.DB_157005_crm7des.Opolog item);
        partial void OnAfterOpologUpdated(QCRM.Models.DB_157005_crm7des.Opolog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Opolog> UpdateOpolog(int idopolog, QCRM.Models.DB_157005_crm7des.Opolog opolog)
        {
            OnOpologUpdated(opolog);

            var itemToUpdate = Context.Opolog
                              .Where(i => i.ID_OPOLOG == opolog.ID_OPOLOG)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(opolog);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterOpologUpdated(opolog);

            return opolog;
        }

        partial void OnOpologDeleted(QCRM.Models.DB_157005_crm7des.Opolog item);
        partial void OnAfterOpologDeleted(QCRM.Models.DB_157005_crm7des.Opolog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Opolog> DeleteOpolog(int idopolog)
        {
            var itemToDelete = Context.Opolog
                              .Where(i => i.ID_OPOLOG == idopolog)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOpologDeleted(itemToDelete);


            Context.Opolog.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOpologDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportOportunidadesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/oportunidades/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/oportunidades/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOportunidadesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/oportunidades/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/oportunidades/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOportunidadesRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Oportunidades> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Oportunidades>> GetOportunidades(Query query = null)
        {
            var items = Context.Oportunidades.AsQueryable();

            items = items.Include(i => i.Contactos);
            items = items.Include(i => i.Contactos1);
            items = items.Include(i => i.Etapas);
            items = items.Include(i => i.Contactos2);
            items = items.Include(i => i.Fabricantes);
            items = items.Include(i => i.Status);
            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Ejecutivos);
            items = items.Include(i => i.Productosinst);
            items = items.Include(i => i.Contactos3);
            items = items.Include(i => i.Tiposerv);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnOportunidadesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOportunidadesGet(QCRM.Models.DB_157005_crm7des.Oportunidades item);
        partial void OnGetOportunidadesByIdOportunidad(ref IQueryable<QCRM.Models.DB_157005_crm7des.Oportunidades> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Oportunidades> GetOportunidadesByIdOportunidad(int idoportunidad)
        {
            var items = Context.Oportunidades
                              .AsNoTracking()
                              .Where(i => i.ID_OPORTUNIDAD == idoportunidad);

            items = items.Include(i => i.Contactos);
            items = items.Include(i => i.Contactos1);
            items = items.Include(i => i.Etapas);
            items = items.Include(i => i.Contactos2);
            items = items.Include(i => i.Fabricantes);
            items = items.Include(i => i.Status);
            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Ejecutivos);
            items = items.Include(i => i.Productosinst);
            items = items.Include(i => i.Contactos3);
            items = items.Include(i => i.Tiposerv);
            items = items.Include(i => i.Usuarios);
 
            OnGetOportunidadesByIdOportunidad(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnOportunidadesGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnOportunidadesCreated(QCRM.Models.DB_157005_crm7des.Oportunidades item);
        partial void OnAfterOportunidadesCreated(QCRM.Models.DB_157005_crm7des.Oportunidades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Oportunidades> CreateOportunidades(QCRM.Models.DB_157005_crm7des.Oportunidades oportunidades)
        {
            OnOportunidadesCreated(oportunidades);

            var existingItem = Context.Oportunidades
                              .Where(i => i.ID_OPORTUNIDAD == oportunidades.ID_OPORTUNIDAD)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Oportunidades.Add(oportunidades);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(oportunidades).State = EntityState.Detached;
                throw;
            }

            OnAfterOportunidadesCreated(oportunidades);

            return oportunidades;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Oportunidades> CancelOportunidadesChanges(QCRM.Models.DB_157005_crm7des.Oportunidades item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnOportunidadesUpdated(QCRM.Models.DB_157005_crm7des.Oportunidades item);
        partial void OnAfterOportunidadesUpdated(QCRM.Models.DB_157005_crm7des.Oportunidades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Oportunidades> UpdateOportunidades(int idoportunidad, QCRM.Models.DB_157005_crm7des.Oportunidades oportunidades)
        {
            OnOportunidadesUpdated(oportunidades);

            var itemToUpdate = Context.Oportunidades
                              .Where(i => i.ID_OPORTUNIDAD == oportunidades.ID_OPORTUNIDAD)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(oportunidades);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterOportunidadesUpdated(oportunidades);

            return oportunidades;
        }

        partial void OnOportunidadesDeleted(QCRM.Models.DB_157005_crm7des.Oportunidades item);
        partial void OnAfterOportunidadesDeleted(QCRM.Models.DB_157005_crm7des.Oportunidades item);

        public async Task<QCRM.Models.DB_157005_crm7des.Oportunidades> DeleteOportunidades(int idoportunidad)
        {
            var itemToDelete = Context.Oportunidades
                              .Where(i => i.ID_OPORTUNIDAD == idoportunidad)
                              .Include(i => i.Actividades)
                              .Include(i => i.Documentos)
                              .Include(i => i.Opolog)
                              .Include(i => i.OportunidadeS5)
                              .Include(i => i.Proyectos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOportunidadesDeleted(itemToDelete);


            Context.Oportunidades.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOportunidadesDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportOportunidadeS5ToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/oportunidades5/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/oportunidades5/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportOportunidadeS5ToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/oportunidades5/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/oportunidades5/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnOportunidadeS5Read(ref IQueryable<QCRM.Models.DB_157005_crm7des.OportunidadeS5> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.OportunidadeS5>> GetOportunidadeS5(Query query = null)
        {
            var items = Context.OportunidadeS5.AsQueryable();

            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnOportunidadeS5Read(ref items);

            return await Task.FromResult(items);
        }

        partial void OnOportunidadeS5Get(QCRM.Models.DB_157005_crm7des.OportunidadeS5 item);
        partial void OnGetOportunidadeS5ById(ref IQueryable<QCRM.Models.DB_157005_crm7des.OportunidadeS5> items);


        public async Task<QCRM.Models.DB_157005_crm7des.OportunidadeS5> GetOportunidadeS5ById(int id)
        {
            var items = Context.OportunidadeS5
                              .AsNoTracking()
                              .Where(i => i.ID == id);

            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Usuarios);
 
            OnGetOportunidadeS5ById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnOportunidadeS5Get(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnOportunidadeS5Created(QCRM.Models.DB_157005_crm7des.OportunidadeS5 item);
        partial void OnAfterOportunidadeS5Created(QCRM.Models.DB_157005_crm7des.OportunidadeS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.OportunidadeS5> CreateOportunidadeS5(QCRM.Models.DB_157005_crm7des.OportunidadeS5 oportunidades5)
        {
            OnOportunidadeS5Created(oportunidades5);

            var existingItem = Context.OportunidadeS5
                              .Where(i => i.ID == oportunidades5.ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.OportunidadeS5.Add(oportunidades5);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(oportunidades5).State = EntityState.Detached;
                throw;
            }

            OnAfterOportunidadeS5Created(oportunidades5);

            return oportunidades5;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.OportunidadeS5> CancelOportunidadeS5Changes(QCRM.Models.DB_157005_crm7des.OportunidadeS5 item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnOportunidadeS5Updated(QCRM.Models.DB_157005_crm7des.OportunidadeS5 item);
        partial void OnAfterOportunidadeS5Updated(QCRM.Models.DB_157005_crm7des.OportunidadeS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.OportunidadeS5> UpdateOportunidadeS5(int id, QCRM.Models.DB_157005_crm7des.OportunidadeS5 oportunidades5)
        {
            OnOportunidadeS5Updated(oportunidades5);

            var itemToUpdate = Context.OportunidadeS5
                              .Where(i => i.ID == oportunidades5.ID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(oportunidades5);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterOportunidadeS5Updated(oportunidades5);

            return oportunidades5;
        }

        partial void OnOportunidadeS5Deleted(QCRM.Models.DB_157005_crm7des.OportunidadeS5 item);
        partial void OnAfterOportunidadeS5Deleted(QCRM.Models.DB_157005_crm7des.OportunidadeS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.OportunidadeS5> DeleteOportunidadeS5(int id)
        {
            var itemToDelete = Context.OportunidadeS5
                              .Where(i => i.ID == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnOportunidadeS5Deleted(itemToDelete);


            Context.OportunidadeS5.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterOportunidadeS5Deleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportPresupuestosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/presupuestos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/presupuestos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportPresupuestosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/presupuestos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/presupuestos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnPresupuestosRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Presupuestos> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Presupuestos>> GetPresupuestos(Query query = null)
        {
            var items = Context.Presupuestos.AsQueryable();

            items = items.Include(i => i.Proyectos);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnPresupuestosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnPresupuestosGet(QCRM.Models.DB_157005_crm7des.Presupuestos item);
        partial void OnGetPresupuestosByIdPto(ref IQueryable<QCRM.Models.DB_157005_crm7des.Presupuestos> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Presupuestos> GetPresupuestosByIdPto(int idpto)
        {
            var items = Context.Presupuestos
                              .AsNoTracking()
                              .Where(i => i.ID_PTO == idpto);

            items = items.Include(i => i.Proyectos);
            items = items.Include(i => i.Usuarios);
 
            OnGetPresupuestosByIdPto(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnPresupuestosGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnPresupuestosCreated(QCRM.Models.DB_157005_crm7des.Presupuestos item);
        partial void OnAfterPresupuestosCreated(QCRM.Models.DB_157005_crm7des.Presupuestos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Presupuestos> CreatePresupuestos(QCRM.Models.DB_157005_crm7des.Presupuestos presupuestos)
        {
            OnPresupuestosCreated(presupuestos);

            var existingItem = Context.Presupuestos
                              .Where(i => i.ID_PTO == presupuestos.ID_PTO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Presupuestos.Add(presupuestos);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(presupuestos).State = EntityState.Detached;
                throw;
            }

            OnAfterPresupuestosCreated(presupuestos);

            return presupuestos;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Presupuestos> CancelPresupuestosChanges(QCRM.Models.DB_157005_crm7des.Presupuestos item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnPresupuestosUpdated(QCRM.Models.DB_157005_crm7des.Presupuestos item);
        partial void OnAfterPresupuestosUpdated(QCRM.Models.DB_157005_crm7des.Presupuestos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Presupuestos> UpdatePresupuestos(int idpto, QCRM.Models.DB_157005_crm7des.Presupuestos presupuestos)
        {
            OnPresupuestosUpdated(presupuestos);

            var itemToUpdate = Context.Presupuestos
                              .Where(i => i.ID_PTO == presupuestos.ID_PTO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(presupuestos);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterPresupuestosUpdated(presupuestos);

            return presupuestos;
        }

        partial void OnPresupuestosDeleted(QCRM.Models.DB_157005_crm7des.Presupuestos item);
        partial void OnAfterPresupuestosDeleted(QCRM.Models.DB_157005_crm7des.Presupuestos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Presupuestos> DeletePresupuestos(int idpto)
        {
            var itemToDelete = Context.Presupuestos
                              .Where(i => i.ID_PTO == idpto)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnPresupuestosDeleted(itemToDelete);


            Context.Presupuestos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterPresupuestosDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportProductosinstToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/productosinst/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/productosinst/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProductosinstToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/productosinst/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/productosinst/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProductosinstRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Productosinst> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Productosinst>> GetProductosinst(Query query = null)
        {
            var items = Context.Productosinst.AsQueryable();

            items = items.Include(i => i.Fabricantes);
            items = items.Include(i => i.Cuentas);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnProductosinstRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProductosinstGet(QCRM.Models.DB_157005_crm7des.Productosinst item);
        partial void OnGetProductosinstByIdProducto(ref IQueryable<QCRM.Models.DB_157005_crm7des.Productosinst> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Productosinst> GetProductosinstByIdProducto(int idproducto)
        {
            var items = Context.Productosinst
                              .AsNoTracking()
                              .Where(i => i.ID_PRODUCTO == idproducto);

            items = items.Include(i => i.Fabricantes);
            items = items.Include(i => i.Cuentas);
 
            OnGetProductosinstByIdProducto(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnProductosinstGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProductosinstCreated(QCRM.Models.DB_157005_crm7des.Productosinst item);
        partial void OnAfterProductosinstCreated(QCRM.Models.DB_157005_crm7des.Productosinst item);

        public async Task<QCRM.Models.DB_157005_crm7des.Productosinst> CreateProductosinst(QCRM.Models.DB_157005_crm7des.Productosinst productosinst)
        {
            OnProductosinstCreated(productosinst);

            var existingItem = Context.Productosinst
                              .Where(i => i.ID_PRODUCTO == productosinst.ID_PRODUCTO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Productosinst.Add(productosinst);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(productosinst).State = EntityState.Detached;
                throw;
            }

            OnAfterProductosinstCreated(productosinst);

            return productosinst;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Productosinst> CancelProductosinstChanges(QCRM.Models.DB_157005_crm7des.Productosinst item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProductosinstUpdated(QCRM.Models.DB_157005_crm7des.Productosinst item);
        partial void OnAfterProductosinstUpdated(QCRM.Models.DB_157005_crm7des.Productosinst item);

        public async Task<QCRM.Models.DB_157005_crm7des.Productosinst> UpdateProductosinst(int idproducto, QCRM.Models.DB_157005_crm7des.Productosinst productosinst)
        {
            OnProductosinstUpdated(productosinst);

            var itemToUpdate = Context.Productosinst
                              .Where(i => i.ID_PRODUCTO == productosinst.ID_PRODUCTO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(productosinst);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProductosinstUpdated(productosinst);

            return productosinst;
        }

        partial void OnProductosinstDeleted(QCRM.Models.DB_157005_crm7des.Productosinst item);
        partial void OnAfterProductosinstDeleted(QCRM.Models.DB_157005_crm7des.Productosinst item);

        public async Task<QCRM.Models.DB_157005_crm7des.Productosinst> DeleteProductosinst(int idproducto)
        {
            var itemToDelete = Context.Productosinst
                              .Where(i => i.ID_PRODUCTO == idproducto)
                              .Include(i => i.Oportunidades)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProductosinstDeleted(itemToDelete);


            Context.Productosinst.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProductosinstDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportProyectosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/proyectos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/proyectos/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProyectosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/proyectos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/proyectos/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProyectosRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Proyectos> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Proyectos>> GetProyectos(Query query = null)
        {
            var items = Context.Proyectos.AsQueryable();

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Tiposproy);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnProyectosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProyectosGet(QCRM.Models.DB_157005_crm7des.Proyectos item);
        partial void OnGetProyectosByProyecto(ref IQueryable<QCRM.Models.DB_157005_crm7des.Proyectos> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Proyectos> GetProyectosByProyecto(string proyecto)
        {
            var items = Context.Proyectos
                              .AsNoTracking()
                              .Where(i => i.PROYECTO == proyecto);

            items = items.Include(i => i.Cuentas);
            items = items.Include(i => i.Oportunidades);
            items = items.Include(i => i.Tiposproy);
            items = items.Include(i => i.Usuarios);
 
            OnGetProyectosByProyecto(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnProyectosGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProyectosCreated(QCRM.Models.DB_157005_crm7des.Proyectos item);
        partial void OnAfterProyectosCreated(QCRM.Models.DB_157005_crm7des.Proyectos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Proyectos> CreateProyectos(QCRM.Models.DB_157005_crm7des.Proyectos proyectos)
        {
            OnProyectosCreated(proyectos);

            var existingItem = Context.Proyectos
                              .Where(i => i.PROYECTO == proyectos.PROYECTO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Proyectos.Add(proyectos);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(proyectos).State = EntityState.Detached;
                throw;
            }

            OnAfterProyectosCreated(proyectos);

            return proyectos;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Proyectos> CancelProyectosChanges(QCRM.Models.DB_157005_crm7des.Proyectos item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProyectosUpdated(QCRM.Models.DB_157005_crm7des.Proyectos item);
        partial void OnAfterProyectosUpdated(QCRM.Models.DB_157005_crm7des.Proyectos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Proyectos> UpdateProyectos(string proyecto, QCRM.Models.DB_157005_crm7des.Proyectos proyectos)
        {
            OnProyectosUpdated(proyectos);

            var itemToUpdate = Context.Proyectos
                              .Where(i => i.PROYECTO == proyectos.PROYECTO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(proyectos);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProyectosUpdated(proyectos);

            return proyectos;
        }

        partial void OnProyectosDeleted(QCRM.Models.DB_157005_crm7des.Proyectos item);
        partial void OnAfterProyectosDeleted(QCRM.Models.DB_157005_crm7des.Proyectos item);

        public async Task<QCRM.Models.DB_157005_crm7des.Proyectos> DeleteProyectos(string proyecto)
        {
            var itemToDelete = Context.Proyectos
                              .Where(i => i.PROYECTO == proyecto)
                              .Include(i => i.Presupuestos)
                              .Include(i => i.ProyectoS5)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProyectosDeleted(itemToDelete);


            Context.Proyectos.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProyectosDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportProyectoS5ToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/proyectos5/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/proyectos5/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportProyectoS5ToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/proyectos5/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/proyectos5/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnProyectoS5Read(ref IQueryable<QCRM.Models.DB_157005_crm7des.ProyectoS5> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.ProyectoS5>> GetProyectoS5(Query query = null)
        {
            var items = Context.ProyectoS5.AsQueryable();

            items = items.Include(i => i.Proyectos);
            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnProyectoS5Read(ref items);

            return await Task.FromResult(items);
        }

        partial void OnProyectoS5Get(QCRM.Models.DB_157005_crm7des.ProyectoS5 item);
        partial void OnGetProyectoS5ById(ref IQueryable<QCRM.Models.DB_157005_crm7des.ProyectoS5> items);


        public async Task<QCRM.Models.DB_157005_crm7des.ProyectoS5> GetProyectoS5ById(int id)
        {
            var items = Context.ProyectoS5
                              .AsNoTracking()
                              .Where(i => i.ID == id);

            items = items.Include(i => i.Proyectos);
            items = items.Include(i => i.Usuarios);
 
            OnGetProyectoS5ById(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnProyectoS5Get(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnProyectoS5Created(QCRM.Models.DB_157005_crm7des.ProyectoS5 item);
        partial void OnAfterProyectoS5Created(QCRM.Models.DB_157005_crm7des.ProyectoS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.ProyectoS5> CreateProyectoS5(QCRM.Models.DB_157005_crm7des.ProyectoS5 proyectos5)
        {
            OnProyectoS5Created(proyectos5);

            var existingItem = Context.ProyectoS5
                              .Where(i => i.ID == proyectos5.ID)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.ProyectoS5.Add(proyectos5);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(proyectos5).State = EntityState.Detached;
                throw;
            }

            OnAfterProyectoS5Created(proyectos5);

            return proyectos5;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.ProyectoS5> CancelProyectoS5Changes(QCRM.Models.DB_157005_crm7des.ProyectoS5 item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnProyectoS5Updated(QCRM.Models.DB_157005_crm7des.ProyectoS5 item);
        partial void OnAfterProyectoS5Updated(QCRM.Models.DB_157005_crm7des.ProyectoS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.ProyectoS5> UpdateProyectoS5(int id, QCRM.Models.DB_157005_crm7des.ProyectoS5 proyectos5)
        {
            OnProyectoS5Updated(proyectos5);

            var itemToUpdate = Context.ProyectoS5
                              .Where(i => i.ID == proyectos5.ID)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(proyectos5);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterProyectoS5Updated(proyectos5);

            return proyectos5;
        }

        partial void OnProyectoS5Deleted(QCRM.Models.DB_157005_crm7des.ProyectoS5 item);
        partial void OnAfterProyectoS5Deleted(QCRM.Models.DB_157005_crm7des.ProyectoS5 item);

        public async Task<QCRM.Models.DB_157005_crm7des.ProyectoS5> DeleteProyectoS5(int id)
        {
            var itemToDelete = Context.ProyectoS5
                              .Where(i => i.ID == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnProyectoS5Deleted(itemToDelete);


            Context.ProyectoS5.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterProyectoS5Deleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportStatusToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/status/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/status/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportStatusToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/status/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/status/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnStatusRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Status> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Status>> GetStatus(Query query = null)
        {
            var items = Context.Status.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnStatusRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnStatusGet(QCRM.Models.DB_157005_crm7des.Status item);
        partial void OnGetStatusByForecast(ref IQueryable<QCRM.Models.DB_157005_crm7des.Status> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Status> GetStatusByForecast(string forecast)
        {
            var items = Context.Status
                              .AsNoTracking()
                              .Where(i => i.FORECAST == forecast);

 
            OnGetStatusByForecast(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnStatusGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnStatusCreated(QCRM.Models.DB_157005_crm7des.Status item);
        partial void OnAfterStatusCreated(QCRM.Models.DB_157005_crm7des.Status item);

        public async Task<QCRM.Models.DB_157005_crm7des.Status> CreateStatus(QCRM.Models.DB_157005_crm7des.Status status)
        {
            OnStatusCreated(status);

            var existingItem = Context.Status
                              .Where(i => i.FORECAST == status.FORECAST)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Status.Add(status);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(status).State = EntityState.Detached;
                throw;
            }

            OnAfterStatusCreated(status);

            return status;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Status> CancelStatusChanges(QCRM.Models.DB_157005_crm7des.Status item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnStatusUpdated(QCRM.Models.DB_157005_crm7des.Status item);
        partial void OnAfterStatusUpdated(QCRM.Models.DB_157005_crm7des.Status item);

        public async Task<QCRM.Models.DB_157005_crm7des.Status> UpdateStatus(string forecast, QCRM.Models.DB_157005_crm7des.Status status)
        {
            OnStatusUpdated(status);

            var itemToUpdate = Context.Status
                              .Where(i => i.FORECAST == status.FORECAST)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(status);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterStatusUpdated(status);

            return status;
        }

        partial void OnStatusDeleted(QCRM.Models.DB_157005_crm7des.Status item);
        partial void OnAfterStatusDeleted(QCRM.Models.DB_157005_crm7des.Status item);

        public async Task<QCRM.Models.DB_157005_crm7des.Status> DeleteStatus(string forecast)
        {
            var itemToDelete = Context.Status
                              .Where(i => i.FORECAST == forecast)
                              .Include(i => i.Oportunidades)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnStatusDeleted(itemToDelete);


            Context.Status.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterStatusDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTiposactToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/tiposact/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/tiposact/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTiposactToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/tiposact/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/tiposact/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTiposactRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Tiposact> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Tiposact>> GetTiposact(Query query = null)
        {
            var items = Context.Tiposact.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTiposactRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTiposactGet(QCRM.Models.DB_157005_crm7des.Tiposact item);
        partial void OnGetTiposactByTipo(ref IQueryable<QCRM.Models.DB_157005_crm7des.Tiposact> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Tiposact> GetTiposactByTipo(string tipo)
        {
            var items = Context.Tiposact
                              .AsNoTracking()
                              .Where(i => i.TIPO == tipo);

 
            OnGetTiposactByTipo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTiposactGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTiposactCreated(QCRM.Models.DB_157005_crm7des.Tiposact item);
        partial void OnAfterTiposactCreated(QCRM.Models.DB_157005_crm7des.Tiposact item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposact> CreateTiposact(QCRM.Models.DB_157005_crm7des.Tiposact tiposact)
        {
            OnTiposactCreated(tiposact);

            var existingItem = Context.Tiposact
                              .Where(i => i.TIPO == tiposact.TIPO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Tiposact.Add(tiposact);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tiposact).State = EntityState.Detached;
                throw;
            }

            OnAfterTiposactCreated(tiposact);

            return tiposact;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposact> CancelTiposactChanges(QCRM.Models.DB_157005_crm7des.Tiposact item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTiposactUpdated(QCRM.Models.DB_157005_crm7des.Tiposact item);
        partial void OnAfterTiposactUpdated(QCRM.Models.DB_157005_crm7des.Tiposact item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposact> UpdateTiposact(string tipo, QCRM.Models.DB_157005_crm7des.Tiposact tiposact)
        {
            OnTiposactUpdated(tiposact);

            var itemToUpdate = Context.Tiposact
                              .Where(i => i.TIPO == tiposact.TIPO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tiposact);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTiposactUpdated(tiposact);

            return tiposact;
        }

        partial void OnTiposactDeleted(QCRM.Models.DB_157005_crm7des.Tiposact item);
        partial void OnAfterTiposactDeleted(QCRM.Models.DB_157005_crm7des.Tiposact item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposact> DeleteTiposact(string tipo)
        {
            var itemToDelete = Context.Tiposact
                              .Where(i => i.TIPO == tipo)
                              .Include(i => i.Actividades)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTiposactDeleted(itemToDelete);


            Context.Tiposact.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTiposactDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTiposdocToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/tiposdoc/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/tiposdoc/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTiposdocToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/tiposdoc/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/tiposdoc/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTiposdocRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Tiposdoc> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Tiposdoc>> GetTiposdoc(Query query = null)
        {
            var items = Context.Tiposdoc.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTiposdocRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTiposdocGet(QCRM.Models.DB_157005_crm7des.Tiposdoc item);
        partial void OnGetTiposdocByTipo(ref IQueryable<QCRM.Models.DB_157005_crm7des.Tiposdoc> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Tiposdoc> GetTiposdocByTipo(string tipo)
        {
            var items = Context.Tiposdoc
                              .AsNoTracking()
                              .Where(i => i.TIPO == tipo);

 
            OnGetTiposdocByTipo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTiposdocGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTiposdocCreated(QCRM.Models.DB_157005_crm7des.Tiposdoc item);
        partial void OnAfterTiposdocCreated(QCRM.Models.DB_157005_crm7des.Tiposdoc item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposdoc> CreateTiposdoc(QCRM.Models.DB_157005_crm7des.Tiposdoc tiposdoc)
        {
            OnTiposdocCreated(tiposdoc);

            var existingItem = Context.Tiposdoc
                              .Where(i => i.TIPO == tiposdoc.TIPO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Tiposdoc.Add(tiposdoc);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tiposdoc).State = EntityState.Detached;
                throw;
            }

            OnAfterTiposdocCreated(tiposdoc);

            return tiposdoc;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposdoc> CancelTiposdocChanges(QCRM.Models.DB_157005_crm7des.Tiposdoc item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTiposdocUpdated(QCRM.Models.DB_157005_crm7des.Tiposdoc item);
        partial void OnAfterTiposdocUpdated(QCRM.Models.DB_157005_crm7des.Tiposdoc item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposdoc> UpdateTiposdoc(string tipo, QCRM.Models.DB_157005_crm7des.Tiposdoc tiposdoc)
        {
            OnTiposdocUpdated(tiposdoc);

            var itemToUpdate = Context.Tiposdoc
                              .Where(i => i.TIPO == tiposdoc.TIPO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tiposdoc);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTiposdocUpdated(tiposdoc);

            return tiposdoc;
        }

        partial void OnTiposdocDeleted(QCRM.Models.DB_157005_crm7des.Tiposdoc item);
        partial void OnAfterTiposdocDeleted(QCRM.Models.DB_157005_crm7des.Tiposdoc item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposdoc> DeleteTiposdoc(string tipo)
        {
            var itemToDelete = Context.Tiposdoc
                              .Where(i => i.TIPO == tipo)
                              .Include(i => i.Documentos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTiposdocDeleted(itemToDelete);


            Context.Tiposdoc.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTiposdocDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTiposervToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/tiposerv/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/tiposerv/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTiposervToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/tiposerv/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/tiposerv/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTiposervRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Tiposerv> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Tiposerv>> GetTiposerv(Query query = null)
        {
            var items = Context.Tiposerv.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTiposervRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTiposervGet(QCRM.Models.DB_157005_crm7des.Tiposerv item);
        partial void OnGetTiposervByTipo(ref IQueryable<QCRM.Models.DB_157005_crm7des.Tiposerv> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Tiposerv> GetTiposervByTipo(string tipo)
        {
            var items = Context.Tiposerv
                              .AsNoTracking()
                              .Where(i => i.TIPO == tipo);

 
            OnGetTiposervByTipo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTiposervGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTiposervCreated(QCRM.Models.DB_157005_crm7des.Tiposerv item);
        partial void OnAfterTiposervCreated(QCRM.Models.DB_157005_crm7des.Tiposerv item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposerv> CreateTiposerv(QCRM.Models.DB_157005_crm7des.Tiposerv tiposerv)
        {
            OnTiposervCreated(tiposerv);

            var existingItem = Context.Tiposerv
                              .Where(i => i.TIPO == tiposerv.TIPO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Tiposerv.Add(tiposerv);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tiposerv).State = EntityState.Detached;
                throw;
            }

            OnAfterTiposervCreated(tiposerv);

            return tiposerv;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposerv> CancelTiposervChanges(QCRM.Models.DB_157005_crm7des.Tiposerv item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTiposervUpdated(QCRM.Models.DB_157005_crm7des.Tiposerv item);
        partial void OnAfterTiposervUpdated(QCRM.Models.DB_157005_crm7des.Tiposerv item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposerv> UpdateTiposerv(string tipo, QCRM.Models.DB_157005_crm7des.Tiposerv tiposerv)
        {
            OnTiposervUpdated(tiposerv);

            var itemToUpdate = Context.Tiposerv
                              .Where(i => i.TIPO == tiposerv.TIPO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tiposerv);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTiposervUpdated(tiposerv);

            return tiposerv;
        }

        partial void OnTiposervDeleted(QCRM.Models.DB_157005_crm7des.Tiposerv item);
        partial void OnAfterTiposervDeleted(QCRM.Models.DB_157005_crm7des.Tiposerv item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposerv> DeleteTiposerv(string tipo)
        {
            var itemToDelete = Context.Tiposerv
                              .Where(i => i.TIPO == tipo)
                              .Include(i => i.Cuotas)
                              .Include(i => i.Oportunidades)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTiposervDeleted(itemToDelete);


            Context.Tiposerv.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTiposervDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportTiposproyToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/tiposproy/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/tiposproy/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportTiposproyToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/tiposproy/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/tiposproy/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnTiposproyRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Tiposproy> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Tiposproy>> GetTiposproy(Query query = null)
        {
            var items = Context.Tiposproy.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnTiposproyRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnTiposproyGet(QCRM.Models.DB_157005_crm7des.Tiposproy item);
        partial void OnGetTiposproyByTipo(ref IQueryable<QCRM.Models.DB_157005_crm7des.Tiposproy> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Tiposproy> GetTiposproyByTipo(string tipo)
        {
            var items = Context.Tiposproy
                              .AsNoTracking()
                              .Where(i => i.TIPO == tipo);

 
            OnGetTiposproyByTipo(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnTiposproyGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnTiposproyCreated(QCRM.Models.DB_157005_crm7des.Tiposproy item);
        partial void OnAfterTiposproyCreated(QCRM.Models.DB_157005_crm7des.Tiposproy item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposproy> CreateTiposproy(QCRM.Models.DB_157005_crm7des.Tiposproy tiposproy)
        {
            OnTiposproyCreated(tiposproy);

            var existingItem = Context.Tiposproy
                              .Where(i => i.TIPO == tiposproy.TIPO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Tiposproy.Add(tiposproy);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(tiposproy).State = EntityState.Detached;
                throw;
            }

            OnAfterTiposproyCreated(tiposproy);

            return tiposproy;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposproy> CancelTiposproyChanges(QCRM.Models.DB_157005_crm7des.Tiposproy item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnTiposproyUpdated(QCRM.Models.DB_157005_crm7des.Tiposproy item);
        partial void OnAfterTiposproyUpdated(QCRM.Models.DB_157005_crm7des.Tiposproy item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposproy> UpdateTiposproy(string tipo, QCRM.Models.DB_157005_crm7des.Tiposproy tiposproy)
        {
            OnTiposproyUpdated(tiposproy);

            var itemToUpdate = Context.Tiposproy
                              .Where(i => i.TIPO == tiposproy.TIPO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(tiposproy);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterTiposproyUpdated(tiposproy);

            return tiposproy;
        }

        partial void OnTiposproyDeleted(QCRM.Models.DB_157005_crm7des.Tiposproy item);
        partial void OnAfterTiposproyDeleted(QCRM.Models.DB_157005_crm7des.Tiposproy item);

        public async Task<QCRM.Models.DB_157005_crm7des.Tiposproy> DeleteTiposproy(string tipo)
        {
            var itemToDelete = Context.Tiposproy
                              .Where(i => i.TIPO == tipo)
                              .Include(i => i.Proyectos)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnTiposproyDeleted(itemToDelete);


            Context.Tiposproy.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterTiposproyDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsuariosToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/usuarios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/usuarios/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsuariosToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/usuarios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/usuarios/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsuariosRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Usuarios> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Usuarios>> GetUsuarios(Query query = null)
        {
            var items = Context.Usuarios.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnUsuariosRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUsuariosGet(QCRM.Models.DB_157005_crm7des.Usuarios item);
        partial void OnGetUsuariosByUsuario(ref IQueryable<QCRM.Models.DB_157005_crm7des.Usuarios> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Usuarios> GetUsuariosByUsuario(string usuario)
        {
            var items = Context.Usuarios
                              .AsNoTracking()
                              .Where(i => i.USUARIO == usuario);

 
            OnGetUsuariosByUsuario(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUsuariosGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUsuariosCreated(QCRM.Models.DB_157005_crm7des.Usuarios item);
        partial void OnAfterUsuariosCreated(QCRM.Models.DB_157005_crm7des.Usuarios item);

        public async Task<QCRM.Models.DB_157005_crm7des.Usuarios> CreateUsuarios(QCRM.Models.DB_157005_crm7des.Usuarios usuarios)
        {
            OnUsuariosCreated(usuarios);

            var existingItem = Context.Usuarios
                              .Where(i => i.USUARIO == usuarios.USUARIO)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Usuarios.Add(usuarios);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(usuarios).State = EntityState.Detached;
                throw;
            }

            OnAfterUsuariosCreated(usuarios);

            return usuarios;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Usuarios> CancelUsuariosChanges(QCRM.Models.DB_157005_crm7des.Usuarios item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUsuariosUpdated(QCRM.Models.DB_157005_crm7des.Usuarios item);
        partial void OnAfterUsuariosUpdated(QCRM.Models.DB_157005_crm7des.Usuarios item);

        public async Task<QCRM.Models.DB_157005_crm7des.Usuarios> UpdateUsuarios(string usuario, QCRM.Models.DB_157005_crm7des.Usuarios usuarios)
        {
            OnUsuariosUpdated(usuarios);

            var itemToUpdate = Context.Usuarios
                              .Where(i => i.USUARIO == usuarios.USUARIO)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(usuarios);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUsuariosUpdated(usuarios);

            return usuarios;
        }

        partial void OnUsuariosDeleted(QCRM.Models.DB_157005_crm7des.Usuarios item);
        partial void OnAfterUsuariosDeleted(QCRM.Models.DB_157005_crm7des.Usuarios item);

        public async Task<QCRM.Models.DB_157005_crm7des.Usuarios> DeleteUsuarios(string usuario)
        {
            var itemToDelete = Context.Usuarios
                              .Where(i => i.USUARIO == usuario)
                              .Include(i => i.Actividades)
                              .Include(i => i.Contactos)
                              .Include(i => i.Ctalog)
                              .Include(i => i.Cuentas)
                              .Include(i => i.CuentaS5)
                              .Include(i => i.Documentos)
                              .Include(i => i.Ejecutivos)
                              .Include(i => i.Notascta)
                              .Include(i => i.Notiflog)
                              .Include(i => i.Opolog)
                              .Include(i => i.Oportunidades)
                              .Include(i => i.OportunidadeS5)
                              .Include(i => i.Presupuestos)
                              .Include(i => i.Proyectos)
                              .Include(i => i.ProyectoS5)
                              .Include(i => i.Usulog)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUsuariosDeleted(itemToDelete);


            Context.Usuarios.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUsuariosDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportUsulogToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/usulog/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/usulog/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportUsulogToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/usulog/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/usulog/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnUsulogRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Usulog> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Usulog>> GetUsulog(Query query = null)
        {
            var items = Context.Usulog.AsQueryable();

            items = items.Include(i => i.Usuarios);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnUsulogRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnUsulogGet(QCRM.Models.DB_157005_crm7des.Usulog item);
        partial void OnGetUsulogByIdLog(ref IQueryable<QCRM.Models.DB_157005_crm7des.Usulog> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Usulog> GetUsulogByIdLog(int idlog)
        {
            var items = Context.Usulog
                              .AsNoTracking()
                              .Where(i => i.ID_LOG == idlog);

            items = items.Include(i => i.Usuarios);
 
            OnGetUsulogByIdLog(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnUsulogGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnUsulogCreated(QCRM.Models.DB_157005_crm7des.Usulog item);
        partial void OnAfterUsulogCreated(QCRM.Models.DB_157005_crm7des.Usulog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Usulog> CreateUsulog(QCRM.Models.DB_157005_crm7des.Usulog usulog)
        {
            OnUsulogCreated(usulog);

            var existingItem = Context.Usulog
                              .Where(i => i.ID_LOG == usulog.ID_LOG)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Usulog.Add(usulog);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(usulog).State = EntityState.Detached;
                throw;
            }

            OnAfterUsulogCreated(usulog);

            return usulog;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Usulog> CancelUsulogChanges(QCRM.Models.DB_157005_crm7des.Usulog item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnUsulogUpdated(QCRM.Models.DB_157005_crm7des.Usulog item);
        partial void OnAfterUsulogUpdated(QCRM.Models.DB_157005_crm7des.Usulog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Usulog> UpdateUsulog(int idlog, QCRM.Models.DB_157005_crm7des.Usulog usulog)
        {
            OnUsulogUpdated(usulog);

            var itemToUpdate = Context.Usulog
                              .Where(i => i.ID_LOG == usulog.ID_LOG)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(usulog);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterUsulogUpdated(usulog);

            return usulog;
        }

        partial void OnUsulogDeleted(QCRM.Models.DB_157005_crm7des.Usulog item);
        partial void OnAfterUsulogDeleted(QCRM.Models.DB_157005_crm7des.Usulog item);

        public async Task<QCRM.Models.DB_157005_crm7des.Usulog> DeleteUsulog(int idlog)
        {
            var itemToDelete = Context.Usulog
                              .Where(i => i.ID_LOG == idlog)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnUsulogDeleted(itemToDelete);


            Context.Usulog.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterUsulogDeleted(itemToDelete);

            return itemToDelete;
        }
    
        public async Task ExportVerticalesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/verticales/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/verticales/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportVerticalesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/db_157005_crm7des/verticales/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/db_157005_crm7des/verticales/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnVerticalesRead(ref IQueryable<QCRM.Models.DB_157005_crm7des.Verticales> items);

        public async Task<IQueryable<QCRM.Models.DB_157005_crm7des.Verticales>> GetVerticales(Query query = null)
        {
            var items = Context.Verticales.AsQueryable();


            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p.Trim());
                    }
                }

                ApplyQuery(ref items, query);
            }

            OnVerticalesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnVerticalesGet(QCRM.Models.DB_157005_crm7des.Verticales item);
        partial void OnGetVerticalesByVertical(ref IQueryable<QCRM.Models.DB_157005_crm7des.Verticales> items);


        public async Task<QCRM.Models.DB_157005_crm7des.Verticales> GetVerticalesByVertical(string vertical)
        {
            var items = Context.Verticales
                              .AsNoTracking()
                              .Where(i => i.VERTICAL == vertical);

 
            OnGetVerticalesByVertical(ref items);

            var itemToReturn = items.FirstOrDefault();

            OnVerticalesGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        partial void OnVerticalesCreated(QCRM.Models.DB_157005_crm7des.Verticales item);
        partial void OnAfterVerticalesCreated(QCRM.Models.DB_157005_crm7des.Verticales item);

        public async Task<QCRM.Models.DB_157005_crm7des.Verticales> CreateVerticales(QCRM.Models.DB_157005_crm7des.Verticales verticales)
        {
            OnVerticalesCreated(verticales);

            var existingItem = Context.Verticales
                              .Where(i => i.VERTICAL == verticales.VERTICAL)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Verticales.Add(verticales);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(verticales).State = EntityState.Detached;
                throw;
            }

            OnAfterVerticalesCreated(verticales);

            return verticales;
        }

        public async Task<QCRM.Models.DB_157005_crm7des.Verticales> CancelVerticalesChanges(QCRM.Models.DB_157005_crm7des.Verticales item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnVerticalesUpdated(QCRM.Models.DB_157005_crm7des.Verticales item);
        partial void OnAfterVerticalesUpdated(QCRM.Models.DB_157005_crm7des.Verticales item);

        public async Task<QCRM.Models.DB_157005_crm7des.Verticales> UpdateVerticales(string vertical, QCRM.Models.DB_157005_crm7des.Verticales verticales)
        {
            OnVerticalesUpdated(verticales);

            var itemToUpdate = Context.Verticales
                              .Where(i => i.VERTICAL == verticales.VERTICAL)
                              .FirstOrDefault();

            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }
                
            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(verticales);
            entryToUpdate.State = EntityState.Modified;

            Context.SaveChanges();

            OnAfterVerticalesUpdated(verticales);

            return verticales;
        }

        partial void OnVerticalesDeleted(QCRM.Models.DB_157005_crm7des.Verticales item);
        partial void OnAfterVerticalesDeleted(QCRM.Models.DB_157005_crm7des.Verticales item);

        public async Task<QCRM.Models.DB_157005_crm7des.Verticales> DeleteVerticales(string vertical)
        {
            var itemToDelete = Context.Verticales
                              .Where(i => i.VERTICAL == vertical)
                              .Include(i => i.Ejecutivos)
                              .Include(i => i.Ejecutivoscta)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnVerticalesDeleted(itemToDelete);


            Context.Verticales.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterVerticalesDeleted(itemToDelete);

            return itemToDelete;
        }
        }
}