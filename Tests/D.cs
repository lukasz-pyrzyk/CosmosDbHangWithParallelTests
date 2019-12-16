using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class D : BaseIntegrationTests
    {
        [Fact]
        public async Task _1()
        {
            await Repository.ReadDatabase();
        }

        [Fact]
        public async Task _2()
        {
            await Repository.ReadDatabase();
        }

        [Fact]
        public async Task _3()
        {
            await Repository.ReadDatabase();
        }

        [Fact]
        public async Task _4()
        {
            await Repository.ReadDatabase();
        }

        [Fact]
        public async Task _5()
        {
            await Repository.ReadDatabase();
        }
    }
}
