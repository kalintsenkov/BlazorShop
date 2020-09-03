namespace BlazorShop.Tests.Mocks
{
    using Moq;
    using MyTested.AspNetCore.Mvc;

    using BlazorShop.Services.Identity;

    public class CurrentUserMock
    {
        public static ICurrentUserService Create
        {
            get
            {
                var moq = new Mock<ICurrentUserService>();

                moq
                    .SetupGet(m => m.UserId)
                    .Returns(TestUser.Identifier);

                return moq.Object;
            }
        }
    }
}
