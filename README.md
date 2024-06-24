# Comment lancer le projet

1. git clone git@github.com:RemiDebruyne/test-csharp-api.git
2. cd .\test-csharp-api\Stereograph.TechnicalTest.Api\
3. dotnet run
4. ouvrez l'un des liens suivants :
   - https://localhost:7163/swagger
   - http://localhost:5163/swagger

## Les difficultés rencontrées

- Je n'avais jamais manipulé de CSV auparavant. J'ai donc rencontré des problèmes avec le seeding de la database. Dans un premier temps avec le code pour setup le seeding (plus de détails dans les commentaires de mes commits) et ensuite pour la correspondance des colonnes et des propriétés de mon `person model`.
- Dans un second temps, j'ai rencontré des problèmes pour le testing des actions de mon controller. Je n'avais jamais fait de mocking, et ait rencontré des problèmes avec la librairie `FakeItEasy`, en passant sur `Moq` j'ai pu progresser dans mes tests unitaires.
- Le dernier problème majeur est venu avec le Dockerfile. Je connais Docker, mais n'ai pas eu beaucoup d'occasion de m'en servir. Le conteneur ne trouve pas le chemin vers `/Ressource/Personnes.csv` et ne se lance pas.

Bien qu'ayant passé plus de temps que conseillé, je souhaitais montrer ma capacité à arriver à un résultat même lorsque cela implique quelque chose avec lequel je n'ai pas ou peu travaillé. J'ai réalisé ce brief seul, sans développeur plus compétent pour m'assister. Dans des conditions réelles, je n'aurais pas passé autant de temps à chercher une solution par moi même.

Comprendre quand il est nécessaire de demander de l'aide aux membres de son équipe, et savoir se débrouiller en autonomie sont deux compétences importantes pour un dévelppeur junior.
