rm -dfr dist
rm -dfr node_modules

rm -dfr App/bin
rm -dfr App/obj

dotnet fable clean App --yes
