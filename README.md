# Description

Provides encryption and decryption (not for everyone, see the documentation) of strings using MD5, SHA1, SHA256, Base 16, Base64, DES, AES algorithms.

# Adding to the project

#### .NET CLI
```CLI
> dotnet add package Hopex.Cipher --version 23.0.1
```

#### Package Manager
```CLI
PM> NuGet\Install-Package Hopex.Cipher -Version 23.0.1
```

#### PackageReference
```XML
<PackageReference Include="Hopex.Cipher" Version="23.0.1" />
```

#### Paket CLI
```CLI
> paket add Hopex.Cipher --version 23.0.1
```

#### Script & Interactive
```CLI
> #r "nuget: Hopex.Cipher, 23.0.1"
```

#### Cake
```
// Install Hopex.Cipher as a Cake Addin
#addin nuget:?package=Hopex.Cipher&version=23.0.1

// Install Hopex.Cipher as a Cake Tool
#tool nuget:?package=Hopex.Cipher&version=23.0.1
```

# Ciphers

| Option | Status |
| --- | ----------- |
| SHA1 | :white_check_mark: |
| SHA256 | :white_check_mark: |
| MD5 | :white_check_mark: |
| Base16 | :white_check_mark: |
| Base64 | :white_check_mark: |
| AES | :white_check_mark: |
| DES | :white_check_mark: |

# How to use

### An example with a search of all types of encryption

```C#
public void Handler()
{
    /**
    * If you use DES encryption, then you need to pass the encryption key to the constructor.
    * For other types of encryption, a key is not required.
    */
    
    string sentence = "Some input sentence";
    string salt = "my-salt";

    Enum
        .GetNames(enumType: typeof(CipherType))
        .ToList()
        .ForEach(action: type =>
    {
        CipherType cipherType = (CipherType)Enum.Parse(
            enumType: typeof(CipherType),
            value: type
        );

        Cipher cipher = new Cipher(
            type: cipherType,
            key: cipherType.Equals(obj: CipherType.DES) ? salt : default
        );

        string encodedInput = cipher.Encode(input: sentence);
        string decodedInput = string.Empty;

        try
        {
            decodedInput = cipher.Decode(input: encodedInput);
        }
        catch (Exception ex)
        {
            decodedInput = ex.Message;
        }

        Console.WriteLine(string.Join(separator: "\n",
            "– – –",
            $"Cipher type: {type}",
            $"Encoded: {encodedInput}",
            $"Decoded: {decodedInput}"
        ));
    });

    /**
    * Console output:
    * 
    * – – –
    * Cipher type: AES
    * Encoded: nWPVHKZ4vyVneN4OfDgA7dHYqCKfBS+XAGykqAkOmGkfaZ08SisazCdrjs9/MHqW
    * Decoded: Some input sentence
    * – – –
    * Cipher type: Base64
    * Encoded: U29tZSBpbnB1dCBzZW50ZW5jZQ==
    * Decoded: Some input sentence
    * – – –
    * Cipher type: DES
    * Encoded: 2/DocoMlaRrp2eH47DD3a+sNSZhFHF3z
    * Decoded: Some input sentence
    * – – –
    * Cipher type: Base16
    * Encoded: 53006f006d006500200069006e007000750074002000730065006e00740065006e0063006500
    * Decoded: Some input sentence
    * – – –
    * Cipher type: MD5
    * Encoded: 2d63e514d848629b2bad0e6022287af8
    * Decoded: MD5 hash cannot be decoded.
    * – – –
    * Cipher type: SHA1
    * Encoded: 284a8d8457bc0f4d512801716a0717e1c29d1605
    * Decoded: SHA1 hash cannot be decoded.
    * – – –
    * Cipher type: SHA256
    * Encoded: e631d3522a22551a28a390e0fa2b71d33e72db062a48d177f26a1d26f36232b6
    * Decoded: SHA-256 hash cannot be decoded.
    */
}
```

### A simple example with two types of encryption

```C#
public void Handler()
{
    /**
    * If you use DES encryption, then you need to pass the encryption key to the constructor.
    * For other types of encryption, a key is not required.
    */
    
    string sentence = "Some input sentence";
    string encodedSentence = string.Empty;
    string salt = "my-salt";
    
    encodedSentence = new Cipher(CipherType.AES).Encode(sentence);
    Console.WriteLine($"AES encoded: {encodedSentence}");
    Console.WriteLine($"AES decoded: {new Cipher(CipherType.AES).Decode(encodedSentence)}");
    
    encodedSentence = new Cipher(CipherType.DES, salt).Encode(sentence);
    Console.WriteLine($"DES encoded: {encodedSentence}");
    Console.WriteLine($"DES decoded: {new Cipher(CipherType.DES, salt).Decode(encodedSentence)}");
}
```

## License

MIT License