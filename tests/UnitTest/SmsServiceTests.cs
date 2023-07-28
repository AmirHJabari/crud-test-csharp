using Application.Common;
using PhoneNumbers;

namespace UnitTest;

public class SmsServiceTests
{
    [Theory]
    [InlineData("+989121234567", true)]
    [InlineData("+982188776655", false)]
    public void ValidatePhoneNumber(string phoneNumber, bool expectedResult)
    {
        Assert.Equal(expectedResult, Validations.IsValidPhoneNumber(phoneNumber));
    }
}