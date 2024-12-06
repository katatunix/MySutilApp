#!/bin/sh -x

dotnet fable App --verbose --run npx vite build $1
