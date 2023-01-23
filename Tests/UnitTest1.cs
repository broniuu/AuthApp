using AuthApp.Interfaces;
using Isopoh.Cryptography.Argon2;
using Moq;

namespace Tests;

public class Tests
{

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PasswordsAreDifferent()
    {
        var passwordToHash = "1234";
        var passwordToVerify = "12345";
        var hashedPassword = Argon2.Hash(passwordToHash);

        Assert.That(!Argon2.Verify(hashedPassword, passwordToVerify));
    }
    [Test]
    public void PasswordsAreSame()
    {
        var passwordToHash = "1234";
        var passwordToVerify = "1234";
        var hashedPassword = Argon2.Hash(passwordToHash);

        Assert.That(Argon2.Verify(hashedPassword, passwordToVerify));
    }
}