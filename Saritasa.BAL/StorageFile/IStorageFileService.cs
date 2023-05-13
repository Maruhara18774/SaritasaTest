using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.StorageFile;
using ViewModels.StorageText;

namespace Saritasa.BAL.StorageFile
{
    public interface IStorageFileService
    {
        Task<string> CreateTextAsync(CreateTextRequest input, Guid userID);
        Task<string> AccessTextAsync(string id, string updatedUser);
        Task<List<TextViewModel>> GetTextsAsync();
        Task<bool> DeleteTextAsync(string id);
    }
}
