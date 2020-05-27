%{
	#include <stdio.h>	
%}

%token NUM MAS MENOS POR DIV PD PI LN IGUAL PCOMA ID 
%start inicio

%%
inicio 	: operaciones
		;
operaciones : ID IGUAL expr PCOMA
			| ID IGUAL expr PCOMA LN operaciones
			| /*Regla Vacia*/
			;
expr : NUM MAS expr
     | NUM MENOS expr
     | NUM POR expr
     | NUM DIV expr
     | PI expr PD
     | NUM
     ;
 %%
 void yyerror(char *cad)
 {
 	printf("__Error sintáctico en %s__",cad);
 }