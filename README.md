# File Generator
Generate large text files with dummy data (i.e. for performance testing an app that needs to read a file). 

This is a console app. It allows to generate large files (you can specify the size in megabytes). 

It has a dictionary of english words (source: https://github.com/dwyl/english-words) which you can replace with your own set. They are picked at random, the generated phrases don't make up meaningful sentences. They repeat occasionally (you can specify the rate at which it happens).

## Usage:
Before you run the app, it needs to be configured. There is a text file called appsettings.json. In it, you need to set mandatory parameters and optionally some optional ones (if they are not present or left empty then default values will be used).

**Mandatory**:
* _FileSizeInMb_ - desired file size in MB (set it to 1000 for a 1 gig file etc.)
* _LineTemplate_ - determines how each line should be generated. More [details on that below](#line-template).

**Optional**:
* _FileSaveLocation_ - folder where the file should be created. Default - the location of the app.
* _FileName_ - the name of the result file. Default: "generated.txt"
* _RepeatRate_ - Applies to words. This setting will make it so that some random set of words in a line will get repeated once per RepeatRate on avarage (the higher the number the less often it will repeat). E.g. if you set it to 100 then you should expect 2 sets of words in in every 100 to be the same. Setting it to 1 means it will repeat all the time. 0 and below is invalid.

**Words**
There is a file **words_alpha.txt** (taken from https://github.com/dwyl/english-words) that I have included in the app. If you want your own set of words, just open it and replace them.

## Line Template:
You can use just text or special tokens. Tokens are surrounded with double curly braces: {{token}}. You can combine these to e.g. create columns for a CSV file. You can arrange the randomInt token so that it resembles a date or a decimal number (see Example 2).

Here is the list of special tokens you can use (explenation in brackets). To some of them you can pass optional parameters, delimited with a colon (:):
* _sequence_ (integer numbers, increasing by 1 with each line)
* _words_ (1-10 english words, picked randomly from the 'words_alpha.txt' file)
* _words:numberOfWords_ (numberOfWords english words, picked randomly from the 'words_alpha.txt' file)
* _words:minWords:maxWords_ (at least minWords up to maxWords english words, picked randomly from the 'words_alpha.txt' file)
* _randomInt_ (random positive integer number)
* _randomInt:minValue_ (random integer number, at least minValue)
* _randomInt:minValue:maxValue_ (random integer number between minValue and maxValue)

**Example 1**:
Creates a file in a csv format (coma separated), with a sequence number, several words (between 1 and 10) and a positive integer number.
```
{{sequence}},{{words}},{{randomInt}}
```

**Result**:
0,omissus coonier,3782
1,tweets gallowglass boterol electrical flippant ventilates,53245
...

**Example 2**:
Creates a file with 1-3 words, a decimal number and a date between year 2000 and 2024.
```
some words: {{words:1:3}} Here's a decimal number: {{randomInt:0:10}}.{{randomInt:0:9}}. And now a date:{{randomInt:2000:2024}}-{{randomInt:1:12}}-{{randomInt:1:28}}
```

**Result**:
some words: unhealthily Here's a decimal number: 8.0. And now a date:2011-1-13
some words: mouch goatsfoot Here's a decimal number: 5.9. And now a date:2015-2-21
...

When the configuration is ready, run the app. If you are using a package that I built, open command line and type:
* on Windows:
```
FileGenerator.exe
```
* on Linux or Mac:
```
./FileGenerator
```