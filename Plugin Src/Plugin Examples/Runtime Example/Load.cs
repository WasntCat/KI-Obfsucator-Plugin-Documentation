using dnlib.DotNet; // Add the DNLIB.dll From the Obsfucators directory
using dnlib.DotNet.Emit;
using KI.obfuscation.IPlugin; // Add the Obsfucator as a Refrance
using Injection; // You can use our Public Injection call from the Obsfucator or Emebed your own inside the assembly
using System;
using System.Linq;

namespace KI_Plugin_Examples
{
    public class Load : IPluginLoader // To Recognize your Assembly as a plugin
    {
        // To finish off and Make the Function
        public void Execute(ModuleDefMD module)
        {
            var Module = ModuleDefMD.Load(typeof(Runtime.InjExample).Module);
            var ct = module.GlobalType.FindOrCreateStaticConstructor();
            var type = Module.ResolveTypeDef(MDToken.ToRID(typeof(Runtime.InjExample).MetadataToken));
            var Member = InjectHelper.Inject(type, module.GlobalType, module);
            var Load = (MethodDef)Member.Single(method => method.Name == "WindowsPopup");
            ct.Body.Instructions.Insert(0, Instruction.Create(OpCodes.Call, Load));
            foreach (var md in module.GlobalType.Methods)
            {
                if (md.Name != ".ctor") continue;
                module.GlobalType.Remove(md);
                break;
            }

            // Renaming Our Runtime Name
            foreach (IDnlibDef def in Member)
            {
                IMemberDef memberDef = def as IMemberDef;

                if ((memberDef as MethodDef) != null)
                    memberDef.Name = Guid.NewGuid().ToString().ToUpper().Substring(0, 5);
                else if ((memberDef as FieldDef) != null)
                    memberDef.Name = Guid.NewGuid().ToString().ToUpper().Substring(0, 5);
            }
        }

    }
}
