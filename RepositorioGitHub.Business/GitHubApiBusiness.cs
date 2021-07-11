using Newtonsoft.Json;
using RepositorioGitHub.Business.Contract;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Dominio.Interfaces;
using RepositorioGitHub.Infra.Contract;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace RepositorioGitHub.Business
{
    public class GitHubApiBusiness : IGitHubApiBusiness
    {
        
        private readonly IContextRepository _context;
        private readonly IGitHubApi _gitHubApi;
        public GitHubApiBusiness(IContextRepository context, IGitHubApi gitHubApi)
        {
            _context = context;
            _gitHubApi = gitHubApi;
        }

        public ActionResult<GitHubRepositoryViewModel> Get()
        {

            var repositorioGit = new ActionResult<GitHubRepositoryViewModel>();            

            var client = new RestClient("https://api.github.com/users/NatySousa/repos");

            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            repositorioGit.Results = (JsonConvert.DeserializeObject<IEnumerable<GitHubRepositoryViewModel>>(response.Content)).ToList();

            if (repositorioGit.Results.Count() > 0 )
            {
                repositorioGit.IsValid = true;
            }

            return repositorioGit;

        }

        public ActionResult<GitHubRepositoryViewModel> GetByNome(string name)
        {
            var repositorioGit = new ActionResult<GitHubRepositoryViewModel>();

            var client = new RestClient("https://api.github.com/repos/NatySousa/" + name);

            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            repositorioGit.Result = JsonConvert.DeserializeObject<GitHubRepositoryViewModel>(response.Content);

            if (repositorioGit.Result != null)
            {
                repositorioGit.IsValid = true;
            }

            return repositorioGit;
        }

        public ActionResult<RepositoryViewModel> GetByName(string name)
        {
            var repositorioGit = new ActionResult<RepositoryViewModel>();

            var client = new RestClient("https://api.github.com/users/" + name + "/repos");

            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);
            
            repositorioGit.Results = (JsonConvert.DeserializeObject<IEnumerable<RepositoryViewModel>>(response.Content)).ToList();

            if (repositorioGit.Results.Count() > 0)
            {
                repositorioGit.IsValid = true;
            }

            return repositorioGit;
        }

        public ActionResult<FavoriteViewModel> GetFavoriteRepository()
        {
            var favorite = new ActionResult<FavoriteViewModel>();
            favorite.Results = new List<FavoriteViewModel>();
            foreach (var item in _context.GetAll())
            {
                var favoriteView = new FavoriteViewModel();
                favoriteView.Description = item.Description;
                favoriteView.Name = item.Name;
                favoriteView.Owner = item.Owner;
                favoriteView.Language = item.Language;
                favoriteView.Id = item.Id;
                favoriteView.UpdateLast = item.UpdateLast;

                favorite.Results.Add(favoriteView);

            }

            favorite.IsValid = true;

            return favorite;
        }

        public ActionResult<GitHubRepositoryViewModel> GetRepository(string owner, string name)
        {
            var repositorioGit = new ActionResult<GitHubRepositoryViewModel>();

            var client = new RestClient("https://api.github.com/repos/" + owner + "/" + name);

            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            repositorioGit.Result = JsonConvert.DeserializeObject<GitHubRepositoryViewModel>(response.Content);

            if (repositorioGit.Result != null)
            {
                repositorioGit.IsValid = true;
            }

            return repositorioGit;
        }

        public ActionResult<FavoriteViewModel> SaveFavoriteRepository(FavoriteViewModel view)
        {
            var favorite = new ActionResult<FavoriteViewModel>();
            var favorito = new Favorite
            {
                Name = view.Name,
                Owner = view.Owner,
                Description = view.Description,
                UpdateLast = view.UpdateLast,
                Language = view.Language
            };

            if (_context.Insert(favorito))
            {
                favorite.IsValid = true;
                favorite.Result = view;
            }

            return favorite;
        }
    }
}
