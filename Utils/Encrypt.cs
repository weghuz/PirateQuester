using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.KeyStore;
using Nethereum.KeyStore.Model;
using Nethereum.Signer;
using Nethereum.Web3.Accounts;

namespace Utils;
public static class Encrypt
{
    public static string GenerateAccount(string password)
    {
        var keyStoreService = new KeyStoreScryptService();
        var ecKey = EthECKey.GenerateKey();
        var keyStore = keyStoreService.EncryptAndGenerateKeyStore(
            password,
            ecKey.GetPrivateKeyAsBytes(),
            ecKey.GetPublicAddress(),
            new ScryptParams { Dklen = 32, N = 32, R = 3, P = 8 });
        return keyStoreService.SerializeKeyStoreToJson(keyStore);
    }

    public static string CreateAccount(string privateKey, string password)
    {
        var keyStoreService = new KeyStoreScryptService();
        var ecKey = new EthECKey(privateKey);
        var keyStore = keyStoreService.EncryptAndGenerateKeyStore(
            password,
            ecKey.GetPrivateKeyAsBytes(),
            ecKey.GetPublicAddress(),
            new ScryptParams { Dklen = 32, N = 32, R = 3, P = 8 });
        return keyStoreService.SerializeKeyStoreToJson(keyStore);
    }

    public static Account GetAccount(string password, string keyStore)
    {
        var keyStoreService = new KeyStoreScryptService();
        var privateKeyBytes = keyStoreService.DecryptKeyStoreFromJson(password, keyStore).ToHex(true);
        var account = new Account(privateKeyBytes);
        return account;
    }
}