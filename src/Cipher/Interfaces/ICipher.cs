namespace Hopex.Cipher.Interfaces;

/// <summary>
///     Contains methods for encrypting and decrypting data.
/// </summary>
public interface ICipher
{
    /// <summary>
    ///     Encryption of input data.
    /// </summary>
    /// <param name="input">Unencrypted string.</param>
    /// <returns>Encrypted string.</returns>
    string Encode(string input);

    /// <summary>
    ///     Decryption of input data.
    /// </summary>
    /// <param name="input">Encrypted string.</param>
    /// <returns>Unencrypted string.</returns>
    string Decode(string input);
}