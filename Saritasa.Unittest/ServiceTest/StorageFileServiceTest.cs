using Microsoft.EntityFrameworkCore;
using Saritasa.BAL.StorageFile;
using Saritasa.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.StorageFile;

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
        public async Task AccessTextAsync_ReadTextAndDeleted()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var id = Guid.NewGuid();
            var dbcontext = new LocalDBContext(builder.Options);
            dbcontext.StorageTexts!.Add(new Data.Entities.StorageText()
            {
                ID = id,
                Content = "Test",
                DownloadOnce = true
            });
            await dbcontext.SaveChangesAsync();

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            // Check read text successfully
            var result = await storageFileService.AccessTextAsync(id.ToString(), "Guest");
            // Check text was deleted
            var result2 = await storageFileService.AccessTextAsync(id.ToString(), "Guest");

            // Assert
            Assert.Equal("Test", result);
            Assert.Equal("", result2);
        }
        [Fact]
        public async Task AccessTextAsync_ReadTextAndNotDeleted()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var id = Guid.NewGuid();
            var dbcontext = new LocalDBContext(builder.Options);
            dbcontext.StorageTexts!.Add(new Data.Entities.StorageText()
            {
                ID = id,
                Content = "Test",
                DownloadOnce = false
            });
            await dbcontext.SaveChangesAsync();

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            // Check read text successfully
            var result = await storageFileService.AccessTextAsync(id.ToString(), "Guest");
            // Check text wasnt deleted
            var result2 = await storageFileService.AccessTextAsync(id.ToString(), "Guest");

            // Assert
            Assert.Equal("Test", result);
            Assert.Equal("Test", result2);
        }

        [Fact]
        public async Task GetTextsAsync_GetListSuccess0()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbcontext = new LocalDBContext(builder.Options);

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.GetTextsAsync();

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetTextsAsync_GetListSuccess2()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbcontext = new LocalDBContext(builder.Options);
            dbcontext.StorageTexts!.Add(new Data.Entities.StorageText()
            {
                ID = Guid.NewGuid(),
                Content = "Test",
                DownloadOnce = false
            });
            dbcontext.StorageTexts!.Add(new Data.Entities.StorageText()
            {
                ID = Guid.NewGuid(),
                Content = "Test",
                DownloadOnce = false
            });
            await dbcontext.SaveChangesAsync();

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.GetTextsAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateTextAsync_CreateSuccess()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbcontext = new LocalDBContext(builder.Options);

            var storageFileService = new StorageFileService(dbcontext);

            var request = new CreateTextRequest()
            {
                Text = "Test",
                DownloadOnce = true
            };
            // Act
            var result = await storageFileService.CreateTextAsync(request, Guid.NewGuid());

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(36, result.Length);
        }

        [Fact]
        public async Task DeleteTextAsync_DeleteSuccess()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var id = Guid.NewGuid();
            var dbcontext = new LocalDBContext(builder.Options);
            dbcontext.StorageTexts!.Add(new Data.Entities.StorageText()
            {
                ID = id,
                Content = "Test"
            });
            await dbcontext.SaveChangesAsync();

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.DeleteTextAsync(id.ToString());

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteTextAsync_TextNotFound()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var id = Guid.NewGuid();
            var dbcontext = new LocalDBContext(builder.Options);

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.DeleteTextAsync(id.ToString());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetServerFileURLAsync_NotFoundFile()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var id = Guid.NewGuid();
            var dbcontext = new LocalDBContext(builder.Options);

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.GetServerFileURLAsync(id.ToString(), "Guest");

            // Assert
            Assert.Equal("", result);
        }
        [Fact]
        public async Task GetServerFileURLAsync_ReadTextAndNotDeleted()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var id = Guid.NewGuid();
            var dbcontext = new LocalDBContext(builder.Options);
            dbcontext.StorageFiles!.Add(new Data.Entities.StorageFile()
            {
                ID = id,
                DownloadOnce = false,
                Url = "Test/URL"
            });
            await dbcontext.SaveChangesAsync();

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.GetServerFileURLAsync(id.ToString(), "Guest");
            var result2 = await storageFileService.GetServerFileURLAsync(id.ToString(), "Guest");

            // Assert
            Assert.Equal("Test/URL", result);
            Assert.Equal("Test/URL", result2);
        }

        [Fact]
        public async Task GetFilesAsync_GetListSuccess0()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbcontext = new LocalDBContext(builder.Options);

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.GetFilesAsync();

            // Assert
            Assert.Empty(result);
        }
        [Fact]
        public async Task GetFilesAsync_GetListSuccess2()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbcontext = new LocalDBContext(builder.Options);
            dbcontext.StorageFiles!.Add(new Data.Entities.StorageFile()
            {
                ID = Guid.NewGuid(),
                Url = "Test",
                DownloadOnce = false
            });
            dbcontext.StorageFiles!.Add(new Data.Entities.StorageFile()
            {
                ID = Guid.NewGuid(),
                Url = "Test",
                DownloadOnce = false
            });
            await dbcontext.SaveChangesAsync();

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.GetFilesAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task CreateFileAsync_CreateSuccess()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var dbcontext = new LocalDBContext(builder.Options);

            var storageFileService = new StorageFileService(dbcontext);

            var request = new CreateTextRequest()
            {
                Text = "Test",
                DownloadOnce = true
            };
            // Act
            var result = await storageFileService.CreateFileAsync(Guid.NewGuid(), Guid.NewGuid(),"Test/Path",false);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteFileAsync_DeleteSuccess()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var id = Guid.NewGuid();
            var dbcontext = new LocalDBContext(builder.Options);
            dbcontext.StorageFiles!.Add(new Data.Entities.StorageFile()
            {
                ID = id,
                Url = "Test"
            });
            await dbcontext.SaveChangesAsync();

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.DeleteFileAsync(id.ToString());

            // Assert
            Assert.True(result);
        }
        [Fact]
        public async Task DeleteFileAsync_TextNotFound()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<LocalDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());

            var id = Guid.NewGuid();
            var dbcontext = new LocalDBContext(builder.Options);

            var storageFileService = new StorageFileService(dbcontext);

            // Act
            var result = await storageFileService.DeleteFileAsync(id.ToString());

            // Assert
            Assert.False(result);
        }
    }
}
