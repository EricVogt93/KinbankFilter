# KinbankFilter Makefile
# Requires: MSBuild (comes with .NET Framework or VS Build Tools)

MSBUILD = msbuild
PROJECT = KinbankFilter/KinbankFilter.csproj
CONFIG = Release
OUTDIR = build

.PHONY: all clean restore build

all: build

restore:
	nuget restore KinbankFilter.sln

build: restore
	$(MSBUILD) $(PROJECT) /p:Configuration=$(CONFIG) /p:OutputPath=../$(OUTDIR)
	@echo Build complete: $(OUTDIR)/KinbankFilter.exe

clean:
	rm -rf $(OUTDIR)
	rm -rf KinbankFilter/bin
	rm -rf KinbankFilter/obj

run: build
	./$(OUTDIR)/KinbankFilter.exe
