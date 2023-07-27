using Application.Common;
using PhoneNumbers;

namespace UnitTest;

public class SmsServiceTests
{
    [Theory]
    [InlineData("+989121234567")]
    [InlineData("+982188776655")]
    public void ValidatePhoneNumber(string phoneNumber)
    {
        var type = Validations.GetPhoneType(phoneNumber);
        switch(type)
        {
            case PhoneNumberType.MOBILE:
                Assert.True(Validations.IsValidPhoneNumber(phoneNumber));
                break;
            // other logics for other types
            default:
                Assert.False(Validations.IsValidPhoneNumber(phoneNumber));
                break;
        }
    }
}