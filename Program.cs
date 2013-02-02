﻿using Microsoft.Web.XmlTransform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XdtSample {
    class Program {
        static void Main(string[] args) {
            if (args == null || args.Length < 3) {
                ShowUsage();
                return;
            }

            string sourceFile = args[0];
            string transformFile = args[1];
            string destFile = args[2];

            if (!File.Exists(sourceFile)) { throw new FileNotFoundException("sourceFile doesn't exist"); }
            if (!File.Exists(transformFile)) { throw new FileNotFoundException("transformFile doesn't exist"); }

            XmlTransformableDocument document = new XmlTransformableDocument();
            document.PreserveWhitespace = true;
            document.Load(sourceFile);

            XmlTransformation transform = new XmlTransformation(transformFile);
            var success = transform.Apply(document);

            document.Save(destFile);

            int exitCode = (success == true) ? 0 : 1;
            Environment.Exit(exitCode);
        }

        static void ShowUsage() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\n\nIncorrect set of arguments");
            sb.AppendLine("\tXdtSample.exe sourceXmlFile transformFile destFile\n\n");
            System.Console.Write(sb.ToString());
        }
    }
}
