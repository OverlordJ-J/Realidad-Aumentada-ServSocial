%{
	//Librerias y codigo de funciones
	#include <stdio.h>
	#include <stdlib.h>
	#include "y.tab.h"
%}
%option noyywrap
%option yylineno

NUM ([0-9]+)
DEC ([0-9]*)+[.]+([0-9]+)
ID ([a-zA-Z_])+([a-zA-Z0-9_]*)
MAS [+]
MENOS [-]
POR [*]
DIV [/]
PI [(]
PD [)]
IGUAL [(=)]
MAYOR [(>)]
MENOR [(<)]
MAYORIG [(>=)]
MENORIG [(<=)]
IGIG [(==)]
DIF [(!=)]
LLAVEOP [({)]
LLAVECL [(})]
LN[\n]
%%
{NUM} {printf("NUM"); return(NUM);}
{DEC} {printf("NUM"); return(NUM);}
{ID}  {printf("ID"); return(ID);}
{MAS} {printf("+"); return(MAS);}
{MENOS} {printf("-"); return(MENOS);}
{POR} {printf("*"); return(POR);}
{DIV} {printf("/"); return(DIV);}
{PI} {printf("("); return(PI);}
{PD} {printf(")"); return(PD);}
{IGUAL} {printf("="); return(IGUAL);}
{MAYOR} {printf(">"); return(MAYOR);}
{MENOR} {printf("<"); return(MENOR);}
{MAYORIG} {printf(">="); return(MAYORIG);}
{MENORIG} {printf("<="); return(MENORIG);}
{IGIG} {printf("=="); return(IGIG);}
{DIF} {printf("!="); return(DIF);}
{LLAVEOP} {printf("{"); return(LLAVEOP);}
{LLAVECL} {printf("}"); return(LLAVECL);}
{LN} {printf("\n"); return(LN);}
. {printf("__Error lexico en: %s__",yytext);}
%%
int main (int argc, char *argv[])
{
	if((yyin=fopen("archivo.txt","r"))==NULL)
	{
		printf("No se pudo abrir el archivo");
		exit(1);
	}
	yyparse(); //Anlisis lexico-sintactico
	return(0);
}