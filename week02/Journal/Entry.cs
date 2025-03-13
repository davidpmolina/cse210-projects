namespace JournalProgram
{
    public class Entry
    {
        public string _prompt { get; set; }
        public string _response { get; set; }
        public string _date { get; set; }

        public Entry(string prompt, string response, string date)
        {
            _prompt = prompt;
            _response = response;
            _date = date;
        }

        public override string ToString()
        {
            return $"Date: {_date}\nPrompt: {_prompt}\nResponse: {_response}\n";
        }
    }
}