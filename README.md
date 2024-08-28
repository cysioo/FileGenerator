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
* _RepeatRate_ - Applies to words and randomInt. This setting will make it so that some random string will get repeated once per StringRepeatRate on avarage (the higher the number the less often it will repeat). E.g. if you set it to 100 then you should expect 2 strings in in every 100 to be the same. Setting it to 1 means it will repeat all the time. 0 and below is invalid.

## Line Template:
You can use just text or special tokens. Tokens are surrounded with double curly braces: {{token}}. You can combine these to e.g. create columns for a CSV file

Here is the list of special tokens you can use (explenation in brackets):
* _sequence_ (integer numbers, increasing by 1 with each line)
* _words_ (several english words, picked randomly)

**Example**:
```
{{sequence}},{{words}}
```

**Result**:
0, omissus coonier
1, tweets gallowglass boterol electrical flippant ventilates
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