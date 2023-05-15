using Microsoft.EntityFrameworkCore;
using Saritasa.BAL.StorageFile;
using Saritasa.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saritasa.Unittest.ServiceTest
{
    public class StorageFileServiceTest
    {
        [Fact]
        public async Task AccessTextAsync_NotFoundText()
        {
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbcontext = new LocalDBContext(builder.Options);

            var storageFileService = new StorageFileService(dbcontext);

            var result = await storageFileService.AccessTextAsync("0A586AC2-DCD4-4E53-6211-08DB53C440D3", "Guest");

            Assert.Equal("", result);
        }

        [Fact]
        public async Task AccessTextAsync_ReadText()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbcontext = new LocalDBContext(builder.Options);
            dbcontext.StorageTexts.Add(new Data.Entities.StorageText()
            {
                ID = new Guid("0A586AC2-DCD4-4E53-6211-08DB53C440D3"),
                Content = "Test",
                DownloadOnce = false
            });
            await dbcontext.SaveChangesAsync();

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            // Check read text successfully
            var result = await storageFileService.AccessTextAsync("0A586AC2-DCD4-4E53-6211-08DB53C440D3", "Guest");
            // Check text was deleted
            //var result2 = await storageFileService.AccessTextAsync("0A586AC2-DCD4-4E53-6211-08DB53C440D3", "Guest");

            // Assert
            Assert.Equal("Test", result);
            //Assert.Equal("", result2);
        }
    }
}
