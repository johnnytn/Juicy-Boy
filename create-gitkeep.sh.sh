#!/usr/bin/env bash

# Cria arquivo .gitkeep em todas as pastas vazias para evitar que o git as delete:
find . -type d -empty -not -path "./.git/*" -exec touch {}/.gitkeep \;