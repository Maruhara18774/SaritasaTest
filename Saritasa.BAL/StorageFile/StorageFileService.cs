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

namespace Saritasa.BAL.StorageFile
{
    public class StorageFileService : IStorageFileService
    {
        private readonly LocalDBContext _dbContext;

        public StorageFileService(LocalDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> AccessText(string id, string updatedUser)
        {
            var file = await _dbContext.StorageTexts.FirstOrDefaultAsync(x => x.ID.ToString() == id);
            file.UpdateByUserID = updatedUser;
            await _dbContext.SaveChangesAsync();
            if (file == null) return "";
            return file.Content;
        }
        public async Task<string> CreateText(CreateTextRequest input, Guid userID)
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
            await _dbContext.StorageTexts.AddAsync(file);
            await _dbContext.SaveChangesAsync();
            return id.ToString();
        }
    }
}
