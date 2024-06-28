using Infrastructure.DataModels;

namespace UnitTests.Builders.DataModelBuilder;

public class UserDataModelBuilder
{
    private readonly UserDataModel _userDataModel = new();

    public UserDataModelBuilder WithId(int id)
    {
        _userDataModel.Id = id;
        return this;
    }

    public UserDataModelBuilder WithUserName(string userName)
    {
        _userDataModel.UserName = userName;
        return this;
    }

    public UserDataModelBuilder WithNormalizeUserName(string normalizeUserName)
    {
        _userDataModel.NormalizedUserName = normalizeUserName;
        return this;
    }

    public UserDataModelBuilder WithEmail(string email)
    {
        _userDataModel.Email = email;
        return this;
    }

    public UserDataModelBuilder WithPasswordHash(string passwordHash)
    {
        _userDataModel.PasswordHash = passwordHash;
        return this;
    }

    public UserDataModelBuilder WithSecurityStamp(string securityStamp)
    {
        _userDataModel.SecurityStamp = securityStamp;
        return this;
    }

    public UserDataModel Build() => _userDataModel;
}
