rm -dfr dist
rm -dfr node_modules

rm -dfr App/bin
rm -dfr App/obj

rm -dfr Sutil.Router2/bin
rm -dfr Sutil.Router2/obj

dotnet fable clean App --yes
