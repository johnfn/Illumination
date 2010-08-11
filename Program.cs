using System;

namespace Illumination {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args) {
            using (Illumination game = new Illumination()) {
                game.Run();
            }
        }
    }
}
