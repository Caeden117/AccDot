using AccDot.Objects;
using UnityEngine;
using Zenject;

namespace AccDot.Installers
{
    
    public class ADWarmupInstaller : MonoInstaller
    {
        public const string coobParentName = "coob parent";

        public override void InstallBindings()
        {
            var coobParent = new GameObject(coobParentName);
            var coob = Instantiate(GameObject.Find("NormalGameNote").transform.Find("NoteCube")).gameObject;
            coob.SetActive(false);
            DontDestroyOnLoad(coob);

            DiContainer AppContainer = Container.ParentContainers[0];

            // When someone asks the butler "give me a Dot",
            //  run the method to create a new dot coob, then give them that cube.
            //  "AsTransient" means the butler will not cache any Dots, it will run the method every time.
            AppContainer.Bind<Dot>().FromMethod(() =>
            {
                var coobGO = Instantiate(coob).gameObject;
                coobGO.SetActive(true); coobGO.GetComponent<MeshRenderer>().enabled = false; // <- caedens fault omegalul

                var circleGlow = coobGO.transform.Find("NoteCircleGlow");
                circleGlow.GetComponent<MeshRenderer>().enabled = true;
                var dot = coobGO.AddComponent<Dot>();
                AppContainer.Inject(dot);
                return dot;
            }).AsTransient();
        }
    }
}
//1.5 lines!



