namespace Hopex.Cipher.Enums;

/// <summary>
///     Type of encryption.
/// </summary>
public enum CipherType
{
    /// <summary>
    ///     The Advanced Encryption Standard (AES), also known by its original name Rijndael.
    /// </summary>
    /// <remarks>
    ///     <see href="https://en.wikipedia.org/wiki/Advanced_Encryption_Standard">Read more</see>
    /// </remarks>
    AES,

    /// <summary>
    ///     The Data Encryption Standard (DES) is a symmetric-key algorithm for the encryption of digital data.
    ///     It is used here cipher mode ECB and padding mode PKCS7.
    /// </summary>
    /// <remarks>
    ///     <see href="https://en.wikipedia.org/wiki/Data_Encryption_Standard">Read more</see>
    /// </remarks>
    DES,

    /// <summary>
    ///     Hexadecimal (or simply HEX) is used in the transfer encoding Base16, in which each byte of the plaintext is broken
    ///     into
    ///     two 4-bit values and represented by two hexadecimal digits.
    /// </summary>
    /// <remarks>
    ///     <see href="https://en.wikipedia.org/wiki/Hexadecimal">Read more</see>
    /// </remarks>
    Base16,

    /// <summary>
    ///     Group of binary-to-text encoding schemes that represent binary data (more specifically, a sequence of
    ///     8-bit bytes) in sequences of 24 bits that can be represented by four 6-bit Base64 digits.
    /// </summary>
    /// <remarks>
    ///     <see href="https://en.wikipedia.org/wiki/Base64">Read more</see>
    /// </remarks>
    Base64,

    /// <summary>
    ///     Base32 is the base-32 numeral system.
    ///     It uses a set of 32 digits, each of which can be represented by 5 bits (25).
    ///     One way to represent Base32 numbers in a human-readable way is by using a standard 32-character set,
    ///     such as the twenty-two upper-case letters A–V and the digits 0-9.
    /// </summary>
    /// <remarks>
    ///     <see href="https://en.wikipedia.org/wiki/Base32">Read more</see>
    /// </remarks>
    Base32,

    /// <summary>
    ///     Message-digest algorithm, cannot be decoded.
    /// </summary>
    /// <remarks>
    ///     <see href="https://en.wikipedia.org/wiki/MD5">Read more</see>
    /// </remarks>
    MD5,

    /// <summary>
    ///     SHA-1 (Secure Hash Algorithm 1) is a hash function which takes an input and produces a 160-bit (20-byte)
    ///     hash value known as a message digest – typically rendered as 40 hexadecimal digits. Cannot be decoded.
    /// </summary>
    /// <remarks>
    ///     <see href="https://en.wikipedia.org/wiki/SHA-1">Read more</see>
    /// </remarks>
    SHA1,

    /// <summary>
    ///     Secure Hash Algorithm 2, includes significant changes from its predecessor, SHA-1. The SHA-2 family
    ///     consists of six hash functions with digests (hash values) that are 224, 256, 384 or 512 bits: SHA-224,
    ///     <see langword="SHA-256" /> (this), SHA-384, SHA-512, SHA-512/224, SHA-512/256. Cannot be decoded.
    /// </summary>
    /// <remarks>
    ///     <see href="https://en.wikipedia.org/wiki/SHA-2">Read more</see>
    /// </remarks>
    SHA256
}