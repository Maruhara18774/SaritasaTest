using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Saritasa.Data.EF;
using Saritasa.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ViewModels.StorageFile;
using ViewModels.StorageText;

namespace Saritasa.BAL.StorageFile
{
    public class StorageFileService : IStorageFileService
    {
        private readonly LocalDBContext _dbContext;

        public StorageFileService(LocalDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AccessTextAsync(string id, string updatedUser)
        {
            // Check text exist
            var file = await _dbContext.StorageTexts!.FirstOrDefaultAsync(x => x.ID.ToString() == id);
            // Text not exist, return
            if (file == null) return "";
            // If DownloadOnce flag was turned on --> Delete, else update the latest user access it
            if (file.DownloadOnce)
            {
                _dbContext.StorageTexts!.Remove(file);
            }
            else
            {
                file.UpdateByUserID = updatedUser;
            }
            await _dbContext.SaveChangesAsync();
            
            return file.Content;
        }
        public async Task<string> CreateTextAsync(CreateTextRequest input, Guid userID)
        {
            var id = Guid.NewGuid();
            var file = new Data.Entities.StorageText()
            {
                ID = id,
                DownloadOnce = input.DownloadOnce,
                Content = input.Text,
                UserID = userID,
                UpdateByUserID = userID.ToString(),
                FileState = FileStateEnum.InUse
            };
            await _dbContext.StorageTexts!.AddAsync(file);
            await _dbContext.SaveChangesAsync();
            return id.ToString();
        }

        public async Task<bool> DeleteTextAsync(string id)
        {
            var file = await _dbContext.StorageTexts!.FirstOrDefaultAsync(x => x.ID.ToString() == id);
            if(file == null) return false;
            _dbContext.StorageTexts!.Remove(file);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<TextViewModel>> GetTextsAsync()
        {
            // Query list of texts
            var query = from t in _dbContext.StorageTexts
                        where t.FileState == FileStateEnum.InUse
                        select t;
            // Convert from db entity to View Model
            var data = await query.Select(
               t => new TextViewModel()
               {
                   ID = t.ID,
                   Content = t.Content,
                   DownloadOnce = t.DownloadOnce
               }).ToListAsync();

            return data;
        }
    }
}
