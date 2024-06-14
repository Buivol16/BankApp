interface TokenInterceptor
{
    string GenerateToken(string[] args);
	Object DecyphToken(string token);
}