using System;
using Illumination.Logic.Missions;

namespace Illumination {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {
            using (Illumination game = new Illumination()) {
                XMLTest.Test();
                game.Run();
            }
        }
    }
}
