// Варіант 7

grammar Calculator;

/*
* Parser Rules
*/

compileUnit : expression EOF;

expression : MMAX LPAREN expression (COMMA expression)* RPAREN #MmaxExpr
           | MMIN LPAREN expression (COMMA expression)* RPAREN #MminExpr
           | LPAREN expression RPAREN #ParenthesizedExpr
           | expression EXPONENT expression #ExponentialExpr
           | expression operatorToken=(MULTIPLY|DIVIDE|DIV|MOD) expression #MultiplicativeExpr
           | expression operatorToken=(ADD|SUBTRACT) expression #AdditiveExpr
           | (ADD|SUBTRACT)? DIGITS #NumberExpr
           | IDENTIFIER #IdentifierExpr
;

/*
* Lexer Rules
*/

DIGITS : [0-9]+;
IDENTIFIER : [A-Z]+[1-9][0-9]*;

EXPONENT : '^';
MULTIPLY : '*';
DIVIDE : '/';
DIV : 'DIV';
MOD : 'MOD';
SUBTRACT : '-';
ADD : '+';
LPAREN : '(';
RPAREN : ')';
MMAX : 'MMAX';
MMIN : 'MMIN';
COMMA : ',';

WS : [ \t\r\n] -> channel(HIDDEN);