# 🔐 КриптоЗахист v1.0

![Версія](https://img.shields.io/badge/версія-1.0-blue)
![Платформа](https://img.shields.io/badge/платформа-Windows-green)
![Мова](https://img.shields.io/badge/мова-C%23-purple)

> Захистіть свої дані за допомогою потужної криптографічної програми, розробленої із сучасними алгоритмами шифрування та дружнім до користувача інтерфейсом!

## 📋 Зміст

- [Про програму](#-про-програму)
- [Технічні деталі](#-технічні-деталі)
- [Як користуватися](#-як-користуватися)
- [Безпека](#-безпека)
- [Розробники](#-розробники)

## 🚀 Про програму

**КриптоЗахист** — це надійне рішення для захисту ваших конфіденційних текстових даних. Програма створена для простого, але надійного шифрування важливої інформації.

### Основні можливості:

- ✅ **Інтуїтивний інтерфейс** — простий у використанні навіть для початківців
- ✅ **Сучасний дизайн** — приємний візуальний вигляд програми
- ✅ **Двостороннє шифрування** — легко шифруйте та дешифруйте дані
- ✅ **Генерація ключів** — автоматичне створення надійних ключів
- ✅ **Збереження файлів** — зберігайте зашифровані дані для використання пізніше

## 💻 Технічні деталі

Під капотом нашої програми працює потужний механізм шифрування:

| Компонент | Використана технологія |
|-----------|------------------------|
| Алгоритм шифрування | AES (з режимом CBC) |
| Довжина ключа | 256 біт |
| Обробка ключів | PBKDF2 (RFC 2898) |
| Перевірка цілісності | SHA-256 |
| Обробка даних | Безпечний потоковий підхід |
| Формат збереження | Захищений JSON-контейнер |

```csharp
// Приклад нашого підходу до шифрування:
using (Aes aes = Aes.Create())
{
    aes.Key = derivedKey;  // Ключ, отриманий через PBKDF2
    aes.GenerateIV();      // Унікальний вектор ініціалізації
    
    // Шифрування з використанням потоків...
}

3. **Secure File Format**
   - Encrypted data stored in JSON format with metadata
   - Version tracking for future compatibility
   - Timestamp for audit purposes
   - Key hash verification
   - Base64 encoding for binary data

4. **Cryptographic Strength Analysis**
   - AES-256 provides 256 bits of security
   - PBKDF2 with 1000 iterations for key derivation
   - Unique IV for each encryption
   - Proper padding and block handling
   - Protection against common attacks:
     - Brute force (through key derivation)
     - Replay attacks (through unique IVs)
     - Key verification to prevent tampering

## Usage

1. Enter the text you want to encrypt in the input textbox
2. Enter your encryption key
3. Click "Encrypt" to encrypt and save the text
4. To decrypt, click "Decrypt" and select the encrypted file
5. Enter the same key used for encryption
6. The decrypted text will appear in the output textbox

## Security Considerations

- The application uses industry-standard cryptographic algorithms
- Keys are derived using PBKDF2 to prevent rainbow table attacks
- Each encryption uses a unique IV to prevent pattern analysis
- File integrity is verified through key hashing
- Error handling prevents information leakage
- Memory is properly cleared after operations

## Technical Details

- Key Length: 256 bits (32 bytes)
- Block Size: 128 bits (16 bytes)
- Key Derivation: PBKDF2 with 1000 iterations
- Hash Algorithm: SHA-256
- File Format: JSON with Base64 encoding
- Encryption Mode: AES-CBC 