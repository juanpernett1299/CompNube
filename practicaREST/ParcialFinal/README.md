# Docker action "Hola Mundo"

Esta acción imprime "Hola Mundo" o "Hola" + el nombre de la persona al log.

## Inputs

### 'who-to-greet'

**Requerido** El nombre de la persona a quien saludar. Por defecto '"Mundo"'.

## Outputs

### 'time'

La fecha en la que se saludó.

## Example usage

uses: juanpernett1299/hello-world-docker-action
with:
  who-to-greet: 'Mona the Octocat'