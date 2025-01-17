# Application de gestion de bibliothèque
GestionBibliothequeAPI est une application RESTful conçu pour la gestion d'une bibliothèque. L'application permet l'utilisateur à faire des opérations de base CRUD (Create, Read, Update, Delete) sur les livres, les auteurs et les prêts.

L'application est construit utilisant une infrastructure .NET, incorporant PostgreSQL comme base de données. L'API est conteneurisé à l'aide de Docker, facilitant le déploiement.

# ENVIRONNEMENT DE TRAVAIL
### Configuration de l'environnement
Avant de commencer, il faut s'assurer que les suivants sont bien installés sur votre machine, préférablement les vérsions les plus récentes:

- .NET SDK
- Microsoft Visual Studio Code
- Docker
- PostgreSQL

### Création du projet GestionBibliothequeAPI
- dotnet new webapi -n GestionBibliothequeAPI
- cd GestionBibliothequeAPI
- dotnet add package Microsoft.EntityFrameworkCore
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
- dotnet add package Swashbuckle.aspNetCore

PostgreSQL doit être démarré lors de l'utilisation de l'application. Cela peut être fait à travers le terminal *PSQL* ou *pgAdmin*, qui vient avec l'installation de PostgreSQL.
Il faudra aussi créer une base de données dans pgAdmin. Dans ce cas, la base de données doit être nommée "BibliothequeDB".

### Configuration du projet
Après la création et modification des fichiers nécessaires (modèles, controlleurs, context.cs, Program.cs et launchSettings.json), il faut créer les migrations et mettre la base de données à jour:

- dotnet ef migrations add InitialCreate
- dotnet ef database update

Finalement, le projet peut être démarré à l'aide de deux commandes.

- Pour avoir accès à une interface simple frontend (http://localhost:5204/): 
dotnet build

- Pour avoir accès aux endpoints API sur Swagger(https://localhost:7295/swagger/index.html): 
dotnet watch -lp https


## COMMANDES POUR DOCKER
Pour construire l'image Docker:
- docker-compose build

Pour démarrer le container Docker:
- docker-compose up

## EXPLICATION DES CHOIX TECHNIQUES EFFECTUÉS
Pour ce projet, je me suis inspiré de plusieurs travaux très similaires que j'ai fait à l'école lorsque j'étudiais .NET ASP Core. 

Parce que le cahier de charges ne mentionnait pas un frontend, j'ai fait un très rapide en prenant une page HTML déjà fait d'un de mes projets.

La base de données est en PostgreSQL. Il fallait simplement faire la connection dans le fichier *appsettings.json*.

L'API est construite autour des principes RESTful, comme que vous aviez demandé. J'ai également utilisé Docker Compose pour orchestrer l'application et la base de données, ce qui simplifie la gestion des conteneurs.

Pour les technologies dont je ne suis pas trop familiers ou en manque de pratique, je me suis fournis 

## DESCRIPTION DES FONCTIONNALITÉS DE L'API

Pour chaque modèle, soit Livre, Auteur et Emprunt, il y a un appel API POST, GET, GET{id}, PUT et DELETE.

- **GET** : Va aller chercher la liste complet de l'objet. Un code 200 va être retourné, ainsi que la liste de l'objet ressemblant à ceci:
[
  {
    "id": 4,
    "nom": "Ali",
    "prenom": "Mahfuzur",
    "dateNaissance": "1996-02-20T05:00:00Z"
  },
  {
    "id": 5,
    "nom": "Sanderson",
    "prenom": "Brandon",
    "dateNaissance": "1996-02-20T05:00:00Z"
  },
  {
    "id": 6,
    "nom": "Snow",
    "prenom": "John",
    "dateNaissance": "2000-05-05T04:00:00Z"
  }
]


- **GET{id}** : Va aller chercher l'objet celon l'ID rentré. Un code 200 va être retourné au succès:
{
  "id": 4,
  "nom": "Ali",
  "prenom": "Mahfuzur",
  "dateNaissance": "1996-02-20T05:00:00Z"
}
Sinon un code d'erreur 404 si le ID n'existe pas:
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
  "title": "Not Found",
  "status": 404,
  "traceId": "00-24870d28d8145fe90e2be9f6c85094d9-aa766373076a2cc0-00"
}


- **POST** : À la création d'un nouveau auteur, livre ou emprunt, le ID de celui-ci sera incrémenté automatiquement de +1, avec un code 201:
{
  "id": 7,
  "nom": "Wayne",
  "prenom": "Bruce",
  "dateNaissance": "2025-01-17T20:43:39.367Z"
}

- **PUT** : À l'aide du ID, les détails de l'objet peut être changé. Un code 204 est retourné avec les changements de l'objet:
curl -X 'PUT' \
  'https://localhost:7295/api/AuteursControlleur/6' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 6,
  "nom": "R. R. Martin",
  "prenom": "George",
  "dateNaissance": "2025-01-17T20:44:50.317Z"
}'

- **DELETE** : Une supression simple en utilisant seulement le ID de l'objet. Un code 200 est simplement retourné pour confirmer la supression.