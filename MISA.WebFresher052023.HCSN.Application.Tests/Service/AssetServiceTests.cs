using AutoMapper;
using MISA.WebFresher052023.HCSN.Application.Service;
using MISA.WebFresher052023.HCSN.Domain;
using MISA.WebFresher052023.HCSN.Domain.Interface;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Application.Tests.Service
{
    [TestFixture]
    public class AssetServiceTests
    {
        #region Fields
        // Khởi tạo các dependency cần thiết cho unit test
        public IFixedAssetRepository assetRepository { get; set; }
        public IMapper mapper { get; set; }
        public IEntityManager assetManager { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        #endregion

        [SetUp]
        public void SetUp()
        {
            // Sử dụng NSubstitute để tạo các instance giả lập cho các dependency
            assetRepository = NSubstitute.Substitute.For<IFixedAssetRepository>();
            mapper = NSubstitute.Substitute.For<IMapper>();
            assetManager = NSubstitute.Substitute.For<IEntityManager>();
        }

        /// <summary>
        /// Unit test trường hợp xóa nhiều bản ghi được truyền 1 danh sách rỗng
        /// </summary>
        /// Created by: LB.Thành (06/08/2023)
        [Test]
        public async Task DeleteManyAsync_EmptyList_ThrowException()
        {
            //Arrange
            var ids = new List<Guid>();
            var expectedMessage = "Không được truyền danh sách rỗng";

            // Tạo instance của AssetService với các dependency đã được giả lập
            var assetService = new FixedAssetService(assetRepository, mapper, assetManager, UnitOfWork);

            // Act & Assert 
            // Kiểm tra xem khi gọi phương thức DeleteManyAsync(ids), có ném ra Exception không
            var exception = Assert.ThrowsAsync<Exception>(async () => await assetService.DeleteManyAsync(ids));

            // Kiểm tra xem thông báo lỗi trong Exception có trùng với thông báo lỗi kỳ vọng không
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            // Kiểm tra xem phương thức BeginTransactionAsync() của UnitOfWork đã được gọi một lần
            await UnitOfWork.Received(1).BeginTransactionAsync();
        }

        /// <summary>
        /// Unit test trường hợp xóa nhiều bản ghi thành công
        /// </summary>
        /// Created by: LB.Thành (06/08/2023)
        [Test]
        public async Task DeleteManyAsync_ListManyItems_Success()
        {
            //Arrange
            var ids = new List<Guid>();
            for (int i = 0; i < 10; i++)
            {
                var newGuid = Guid.NewGuid();
                ids.Add(newGuid);
            }

            var assets = new List<FixedAsset>();
            for (int i = 0; i < 10; i++)
            {
                var asset = new FixedAsset();
                assets.Add(asset);
            }

            // Giả lập hành vi của repository: GetListByIdsAsync(ids) sẽ trả về danh sách các bản ghi có Id tương ứng
            assetRepository.GetListByIdsAsync(ids).Returns(assets);

            // Tạo instance của AssetService với các dependency đã được giả lập
            var assetService = new FixedAssetService(assetRepository, mapper, assetManager, UnitOfWork);

            // Act
            // Gọi phương thức DeleteManyAsync(ids) để kiểm tra xem có gặp lỗi hay không
            await assetService.DeleteManyAsync(ids);

            // Assert 
            // Kiểm tra xem phương thức GetListByIdsAsync(ids) của repository đã được gọi một lần
            await assetRepository.Received(1).GetListByIdsAsync(ids);

            // Kiểm tra xem phương thức DeleteManyAsync(assets) của repository đã được gọi một lần
            await assetRepository.Received(1).DeleteManyAsync(assets);

            // Kiểm tra xem phương thức CommitAsync() của UnitOfWork đã được gọi một lần
            await UnitOfWork.Received(1).CommitAsync();
        }

        /// <summary>
        /// Unit test trường hợp xóa nhiều bản ghi trong đó có bản ghi lỗi
        /// </summary>
        /// Created by: LB.Thành (06/08/2023)
        [Test]
        public async Task DeleteManyAsync_ListManyItems_ThrowException()
        {
            //Arrange
            // Tạo danh sách các Id để xóa
            var ids = new List<Guid>();
            for (int i = 0; i < 10; i++)
            {
                var newGuid = Guid.NewGuid();
                ids.Add(newGuid);
            }

            // Tạo danh sách các bản ghi tương ứng với các Id
            var assets = new List<FixedAsset>();
            for (int i = 0; i < 9; i++)
            {
                var asset = new FixedAsset();
                assets.Add(asset);
            }

            // Giả lập hành vi của repository: GetListByIdsAsync(ids) sẽ trả về danh sách các bản ghi có Id tương ứng
            assetRepository.GetListByIdsAsync(ids).Returns(assets);

            // Tạo thông báo lỗi kỳ vọng
            var expectedMessage = "Không thể xóa";

            // Tạo instance của AssetService với các dependency đã được giả lập
            var assetService = new FixedAssetService(assetRepository, mapper, assetManager, UnitOfWork);

            // Act & Assert 
            // Kiểm tra xem khi gọi phương thức DeleteManyAsync(ids), có ném ra Exception không
            var exception = Assert.ThrowsAsync<Exception>(async () => await assetService.DeleteManyAsync(ids));

            // Kiểm tra xem thông báo lỗi trong Exception có trùng với thông báo lỗi kỳ vọng không
            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            // Kiểm tra xem phương thức GetListByIdsAsync(ids) của repository đã được gọi một lần
            await assetRepository.Received(1).GetListByIdsAsync(ids);

            // Kiểm tra xem phương thức BeginTransactionAsync() của UnitOfWork đã được gọi một lần
            await UnitOfWork.Received(1).BeginTransactionAsync();
        }

    }
}

