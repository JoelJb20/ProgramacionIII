using System.Text.Json;

namespace Application.DTO
{
    public class ResponseManager
    {
        public bool Succeded => !Warnings.Any();
        public List<string> Warnings { get; set; } = new List<string>(0);
        public int Identity { get; set; }
    }

    public class ResponseManager<T> : ResponseManager where T : class
    {
        public List<T> DataList { get; set; } = new List<T>();
        public T? SingleData { get; set; }

    }


}
