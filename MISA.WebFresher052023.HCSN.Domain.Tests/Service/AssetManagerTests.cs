using MISA.WebFresher052023.HCSN.Domain.Interface;
using MISA.WebFresher052023.HCSN.Domain.Tests.Service;
using MISA.WebFresher052023.HCSN.Infrastructure;
using MISA.WebFresher052023.HCSN.Infrastructure.UnitOfWork;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023.HCSN.Domain.Tests
{
    [TestFixture]
    public class AssetManagerTests
    {
        /// <summary>
        /// Kiểm tra xem phương thức CheckExistedAssetByCode có đưa ra ngoại lệ ConflictException 
        /// khi tìm thấy tài sản tồn tại với mã code đã cho.
        /// </summary>
        /// Created by: LB.Thành (06/08/2023)
        [Test]
        public async Task CheckExistedAssetByCode_ExistAsset_ConflictException()
        {
            // Chuẩn bị dữ liệu giả:
            var code = "Hello";

            // Tạo một mock cho interface IAssetRepository sử dụng NSubstitute.
            var assetRepository = NSubstitute.Substitute.For<IFixedAssetRepository>();

            // Giả lập hành vi của phương thức FindByCodeAsync trả về một tài sản (Asset) giả.
            assetRepository.FindByCodeAsync(code).Returns(new FixedAsset());

            // Khởi tạo một đối tượng AssetManager với mock repository để kiểm tra phương thức CheckExistedAssetByCode.
            var assetManager = new FixedAssetManager(assetRepository);

            // Thực hiện phương thức CheckExistedAssetByCode và kiểm tra xem có đưa ra ngoại lệ ConflictException hay không.
            Assert.ThrowsAsync<ConflictException>(async () => await assetManager.CheckExistedEntityByCode(code));

            // Xác thực kết quả bằng cách kiểm tra xem phương thức FindByCodeAsync đã được gọi với đúng tham số code một lần.
            assetRepository.Received(1).FindByCodeAsync(code);
        }

        /// <summary>
        /// Đây là unit test kiểm tra xem phương thức CheckExistedAssetByCode 
        /// hoạt động chính xác khi không tìm thấy tài sản nào với mã code đã cho.
        /// </summary>
        /// Created by: LB.Thành (06/08/2023)
        [Test]
        public async Task CheckExistedAssetByCode_NotExistAsset_Success()
        {
            // Chuẩn bị dữ liệu giả để test
            var code = "Hello";

            // Khởi tạo một đối tượng AssetRepositoryTests để mock phương thức TotalCall.
            var assetRepositortyTests = new AssetRepositoryTests();

            // Khởi tạo một đối tượng AssetManager với mock repository để kiểm tra phương thức CheckExistedAssetByCode.
            var assetManager = new FixedAssetManager(assetRepositortyTests);

            // Thực hiện phương thức CheckExistedAssetByCode.
            await assetManager.CheckExistedEntityByCode(code);

            // Xác thực kết quả bằng cách kiểm tra giá trị của TotalCall trong assetRepositortyTests.
            Assert.That(assetRepositortyTests.TotalCall, Is.EqualTo(1));
        }
    }
}
