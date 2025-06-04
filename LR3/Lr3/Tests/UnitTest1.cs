namespace Tests
{
    public class StudentValidatorTests
    {
        private readonly StudentValidator _validator = new StudentValidator();

        [Fact]
        public void Validate_ValidStudent_DoesNotThrow()
        {
            
            var validStudent = new StudentDTO { Name = "Иван Иванов", Grade = 8 };

           
            var exception = Record.Exception(() => _validator.Validate(validStudent));

            Assert.Null(exception);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Validate_EmptyName_ThrowsException(string name)
        {
            
            var invalidStudent = new StudentDTO { Name = name, Grade = 5 };

            
            var ex = Assert.Throws<ArgumentException>(() => _validator.Validate(invalidStudent));
            Assert.Contains("не может быть пустым", ex.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        [InlineData(100)]
        public void Validate_InvalidGrade_ThrowsException(int grade)
        {
            
            var invalidStudent = new StudentDTO { Name = "Петр Петров", Grade = grade };

            
            var ex = Assert.Throws<ArgumentException>(() => _validator.Validate(invalidStudent));
            Assert.Contains("должна быть между 0 и 10", ex.Message);
        }

    }

    public class QuoteApiIntegrationTests : IAsyncLifetime
    {
        private HttpClient _httpClient;
        private QuoteAPIAdapter _adapter;

        public Task InitializeAsync()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://zenquotes.io/api/random")
            };
            _adapter = new QuoteAPIAdapter(_httpClient);
            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            _httpClient.Dispose();
            return Task.CompletedTask;
        }

        [Fact]
        public async Task GetRandomQuoteAsync_WithRealApi_ShouldReturnValidQuote()
        {
          
            var quote = await _adapter.GetRandomQuoteAsync();

        
            Assert.NotNull(quote);
            Assert.False(string.IsNullOrEmpty(quote.Content));
            Assert.False(string.IsNullOrEmpty(quote.Author));
        }

        [Fact]
        public async Task GetRandomQuoteAsync_WithRealApi_ShouldReturnDifferentQuotes()
        {
       
            var quote1 = await _adapter.GetRandomQuoteAsync();
            var quote2 = await _adapter.GetRandomQuoteAsync();


            Assert.NotEqual(quote1.Content, quote2.Content);
        }
    }
}