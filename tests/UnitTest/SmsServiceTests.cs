using Application.Common;
using PhoneNumbers;

namespace UnitTest;

public class SmsServiceTests
{
    [Theory]
    [InlineData("+989121234567", PhoneNumberType.MOBILE)]
    [InlineData("+982188776655", PhoneNumberType.FIXED_LINE)]
    public void ValidatePhoneNumber(string phoneNumber, PhoneNumberType expectedType)
    {
        var type = Validations.GetPhoneType(phoneNumber);

        Assert.Equal(expectedType, type);
        Assert.Equal(Validations.IsValidPhoneNumber(phoneNumber), PhoneNumberType.MOBILE == type);
    }
}