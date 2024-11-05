﻿using NUnit.Framework;
using SecurityEssentials.Core;

namespace SecurityEssentials.Unit.Tests.Core.Security
{
	/// <summary>
	/// Summary description for Encryption2
	/// </summary>
	[TestFixture]
	public class EncryptionTests
	{


		private readonly Encryption _testEncrypt;
		private readonly Encryption _testDecrypt;
		private string _password = "qrstuvwxzy1234567890";
		private int _iterationCount = 68;

		public EncryptionTests()
		{

			_testEncrypt = new Encryption();
			_testDecrypt = new Encryption();

		}

		[TearDown]
		public void MyTestCleanup()
		{
			_testEncrypt.Dispose();
			_testDecrypt.Dispose();
		}

		[Test]
		public void EnDecrypt_BasicMessage_Succeeds()
		{

			string input = "John Was here John was here again";
            Assert.That(_testEncrypt.Encrypt(_password, _iterationCount, input, out var salt, out var encrypted), Is.True, "Encryption was not successful");
			Assert.That(input, Is.Not.EqualTo(encrypted));
			Assert.That(_testDecrypt.Decrypt(_password, salt, _iterationCount, encrypted, out var output), "Decryption was not successful");
			Assert.That(input, Is.EqualTo(output));

		}

		[Test]
		public void EnDecrypt_ComplexMessage_Succeeds()
		{

			string input = "john was here 1234567890!£$%^&**()-_=+[]{}';,./#:@~?><  \\|`¬";
            Assert.That(_testEncrypt.Encrypt(_password, _iterationCount, input, out var salt, out var encrypted), "Encryption was not successful");
			Assert.That(input, Is.Not.EqualTo(encrypted));
			Assert.That(_testDecrypt.Decrypt(_password, salt, _iterationCount, encrypted, out var output), "Decryption was not successful");
			Assert.That(input, Is.EqualTo(output));

		}

		[Test]
		public void EnDecrypt_BlankString_Succeeds()
		{

			string input = "";
            Assert.That(true, Is.EqualTo(_testEncrypt.Encrypt(_password, _iterationCount, input, out var salt, out var encrypted)));
			Assert.That(input, Is.Not.EqualTo(encrypted));
			Assert.That(true, Is.EqualTo(_testDecrypt.Decrypt(_password, salt, _iterationCount, encrypted, out var output)));
			Assert.That(input, Is.EqualTo(output));

		}

		[Test]
		public void Encrypt_Null_DecryptsToZeroLengthString()
		{

			string input = null;
            Assert.That(true, Is.EqualTo(_testEncrypt.Encrypt(_password, _iterationCount, input, out var salt, out var encrypted)));
			Assert.That(true, Is.EqualTo(_testDecrypt.Decrypt(_password, salt, _iterationCount, encrypted, out var output)));
			Assert.That(input, Is.Not.EqualTo(output));
			Assert.That("", Is.EqualTo(output));

		}

		[Test]
		public void EnDecrypt_Message_ChangeIterations_Fails()
		{

			string input = "The quick brown fox jumped over the lazy dog";
            Assert.That(true, Is.EqualTo(_testEncrypt.Encrypt(_password, _iterationCount, input, out var salt, out var encrypted)));
			Assert.That(input, Is.Not.EqualTo(encrypted));
			Assert.That(true, Is.Not.EqualTo(_testDecrypt.Decrypt(_password, salt, _iterationCount + 1, encrypted, out var output)));
			Assert.That(input, Is.Not.EqualTo(output));

		}

		[Test]
		public void EnDecrypt_Message_ChangeSalt_Fails()
		{

			string input = "The quick brown fox jumped over the lazy dog";
            Assert.That(true, Is.EqualTo(_testEncrypt.Encrypt(_password, _iterationCount, input, out var salt, out var encrypted)));
			Assert.That(input, Is.Not.EqualTo(encrypted));
			Assert.That(true, Is.Not.EqualTo(_testDecrypt.Decrypt(_password, $"{salt}1", _iterationCount, encrypted, out var output)));
			Assert.That(input, Is.Not.EqualTo(output));

		}

		[Test]
		public void EnDecrypt_Message_ChangePassword_Fails()
		{

			string input = "The quick brown fox jumped over the lazy dog";
            Assert.That(true, Is.EqualTo(_testEncrypt.Encrypt(_password, _iterationCount, input, out var salt, out var encrypted)));
			Assert.That(input, Is.Not.EqualTo(encrypted));
			Assert.That(true, Is.Not.EqualTo(_testDecrypt.Decrypt($"{_password}1", salt, _iterationCount, encrypted, out var output)));
			Assert.That(input, Is.Not.EqualTo(output));
		}

	}
}
