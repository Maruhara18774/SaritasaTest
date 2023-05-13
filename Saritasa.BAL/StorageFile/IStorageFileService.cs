using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.StorageFile;

namespace Saritasa.BAL.StorageFile
{
    public interface IStorageFileService
    {
        Task<string> CreateText(CreateTextRequest input, Guid userID);
        Task<string> AccessText(string id, string updatedUser);
    }
}
