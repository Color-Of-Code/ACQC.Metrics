#include "stdafx.h"

/*
	This file has
	93 total lines
	45 LLOC
  21 comment lines
	13 blank lines
	7  procs
	11 CC
	6  DC
*/

/***** this is a one line block comment *****/
/* embracing comment */ int k; /*embracing comment*/
const char c = '('; // trailing comment
/* comment
*/ int h2; /* more comment */

#define MAX_MAP_CLICK_POLYGON_NAME_LEN			(32)
typedef struct PgMgrRdSegInfo__
{};

class testclass {
	int k;
	testclass::testclass() : k(0) {}
	tesclass::~testclass()
	{
/************************************************/
// above is a star comment line				
	}	
}

int funcTwo(int s, std::string ss);
/**/ // very small comment
void code(void) {}

namespace kkk {
}

char **
word_list_to_argv (list, copy, starting_index, ip)
     WORD_LIST *list;
     int copy, starting_index, *ip;
{
	return 0;
}

std::string boing("hallo" + " welt"
				  " \t\n\"'@><=;|&(");

/* this is a one line block comment */
myType* funcOne (
			  int i, 
			  int a,
			  float b,
			  std::string halloWelt
			  )
{ 
	code(/*embedded comment*/);
	code();
	funcTwo(i, "test/**///");
	funcTwo(i, "test/*");
/*comment*/}

int funcTwo(int s, std::string ss)
{ 
	code(); /*embedded comment*/
	if (true){}
	if (true)
	{	
		code();
		if (false)
		{
			code();
		}
		else if(true)
		{
			code();
		}
	}
	code();
	code();
	return 0;}

void funcThree()
{
code();	

				code();;
}

// end