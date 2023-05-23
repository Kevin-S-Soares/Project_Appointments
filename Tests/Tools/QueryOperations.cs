using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;

namespace ModelTests.Tools
{
    public static class QueryOperations
    {
        public static void BindData<T>(this Mock<DbSet<T>> mock, IQueryable<T> data) where T : class
        {
            mock.As<IQueryable<T>>().Setup(x => x.Provider)
                .Returns(data.Provider);
            mock.As<IQueryable<T>>().Setup(x => x.ElementType)
                .Returns(data.ElementType);
            mock.As<IQueryable<T>>().Setup(x => x.Expression)
                .Returns(data.Expression);
            mock.As<IQueryable<T>>().Setup(x => x.GetEnumerator())
                .Returns(data.GetEnumerator());
        }
    }
}
