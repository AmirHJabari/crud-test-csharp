using PhoneNumbers;

namespace Application.Common;

public class Validations
{
    public static bool IsValidPhoneNumber(short phoneCountryCode, long num)
        => IsValidPhoneNumber($"+{phoneCountryCode}{num}");

    public static PhoneNumberType? GetPhoneType(string phoneNumber)
    {
        try
        {
            var instace = PhoneNumberUtil.GetInstance();
            var number = instace.Parse(phoneNumber, "");
            return instace.GetNumberType(number);
        }
        catch
        {
            return null;
        }
    }

    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        var type = GetPhoneType(phoneNumber);
        if (type is null) return false;

        switch (type)
        {
            case PhoneNumberType.MOBILE:
                return true;

            case PhoneNumberType.FIXED_LINE:
            case PhoneNumberType.FIXED_LINE_OR_MOBILE:
            case PhoneNumberType.TOLL_FREE:
            case PhoneNumberType.PREMIUM_RATE:
            case PhoneNumberType.SHARED_COST:
            case PhoneNumberType.VOIP:
            case PhoneNumberType.PERSONAL_NUMBER:
            case PhoneNumberType.PAGER:
            case PhoneNumberType.UAN:
            case PhoneNumberType.VOICEMAIL:
            case PhoneNumberType.UNKNOWN:
            default:
                return false;
        }
    }
}
