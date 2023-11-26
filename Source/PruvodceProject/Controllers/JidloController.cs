﻿using Microsoft.AspNetCore.Mvc;
using PruvodceProject.Data;
using PruvodceProject.Models;

namespace PruvodceProject.Controllers
{
    public class JidloController : Controller
    {
        PruvodceData Databaze { get; }
        
        public JidloController(PruvodceData databaze)
        {
            Databaze = databaze;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Automaty()
        {
            List<AutomatyModel> automaty = Databaze.Automaty.ToList();
            automaty.Add(new AutomatyModel()
            {
                budova = "Školní 101",
                patro = "1",
                typ = "Automat na jídlo",
                bagety = true

            }); 
            return View(automaty);
        }

        public IActionResult Kafe()
        {
            //string clanek = "{\"ops\":[{\"insert\":\"Při tvorbě dokumentů existují jistá pravidla, která bychom měli všichni dodržovat, už jen proto, že když je všechny dodržíme, bude se naše dílo číst mnohem lépe a i tak (mnohem lépe) bude vypadat.\\nPojďme si společně některá tato pravidla zopakovat!\\n\\nStřídmost s fonty i barvami\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"Stejně jako u střídmosti s jídlem a pitím, platí i střídmost s fonty a barvami. Jednoduše řečeno, všeho moc škodí.\\nNení výjimečné, že se v jednom dokumentu vystřídá fontů několik a barevný je jak pouťová atrakce. \\n\"},{\"insert\":{\"image\":\"//d8-a.sdn.cz/d_8/c_imgrestricted_QR_F/t68k4/mix-fontu.png?fl=cro,0,0,1920,1080|res,600,,3\"}},{\"insert\":\"\\nUkázka mixování fontů\\nJak je to s fonty?\"},{\"attributes\":{\"header\":3},\"insert\":\"\\n\"},{\"insert\":\"Na obrázku výše vidíme možný příklad míchání fontů (záměrně přehnaný). Je to takový nepěkná matlanina. Že?\\nIdeální je, nepoužívat v našem díle více než 3 fonty, ale ten čtvrtý bychom taky nějak přežili. V rozsáhlejších pracích pak používáme jeden bezpatkový (příkladem může být všeobecně známé Calibri nebo na našem \"},{\"attributes\":{\"link\":\"https://comptelo.com\"},\"insert\":\"webu\"},{\"insert\":\" používané Roboto), který budeme mít primárně vyhrazený pro nadpisy a jeden patkový (všemi známý Times New Roman nebo námi používaný Roboto Slab). Ten bude vyhrazený pro samotné „tělo“ textu.\\nVyhněme se barevném cirkusu\"},{\"attributes\":{\"header\":3},\"insert\":\"\\n\"},{\"insert\":\"Dalším častým nešvarem je používání příliš velkého počtu barev. Stejně jako u fontů platí 3 maximálně 4 barvy na dokument.\\nTaké bychom se vyhnuli tomu, aby se barvy používali pro časté zvýrazňování textu. Text sice zvýrazníte, ale při nadměrném užití se opět dostaneme do stádia, kdy je náš dokument barevný jak papoušek. Pro zvýraznění textu můžeme použít kurzívu, tučný text, nebo podtržení.\\nTip: při formátování si můžete práci ulehčit třeba pomocí stylů ve Wordu!\"},{\"attributes\":{\"blockquote\":true},\"insert\":\"\\n\"},{\"insert\":\"\\nJedna mezera stačí, drahoušku\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"Toto je bohužel stále častý jev, který se vyskytuje v nejednom příspěvku na Facebooku, síti X (v našich srdcích stále Twitteru), ročníkové práci či úředním rozhodnutí. \\n\\n\"},{\"insert\":{\"video\":\"https://platform.twitter.com/embed/Tweet.html?dnt=false&embedId=twitter-widget-2&features=eyJ0ZndfdGltZWxpbmVfbGlzdCI6eyJidWNrZXQiOltdLCJ2ZXJzaW9uIjpudWxsfSwidGZ3X2ZvbGxvd2VyX2NvdW50X3N1bnNldCI6eyJidWNrZXQiOnRydWUsInZlcnNpb24iOm51bGx9LCJ0ZndfdHdlZXRfZWRpdF9iYWNrZW5kIjp7ImJ1Y2tldCI6Im9uIiwidmVyc2lvbiI6bnVsbH0sInRmd19yZWZzcmNfc2Vzc2lvbiI6eyJidWNrZXQiOiJvbiIsInZlcnNpb24iOm51bGx9LCJ0ZndfZm9zbnJfc29mdF9pbnRlcnZlbnRpb25zX2VuYWJsZWQiOnsiYnVja2V0Ijoib24iLCJ2ZXJzaW9uIjpudWxsfSwidGZ3X21peGVkX21lZGlhXzE1ODk3Ijp7ImJ1Y2tldCI6InRyZWF0bWVudCIsInZlcnNpb24iOm51bGx9LCJ0ZndfZXhwZXJpbWVudHNfY29va2llX2V4cGlyYXRpb24iOnsiYnVja2V0IjoxMjA5NjAwLCJ2ZXJzaW9uIjpudWxsfSwidGZ3X3Nob3dfYmlyZHdhdGNoX3Bpdm90c19lbmFibGVkIjp7ImJ1Y2tldCI6Im9uIiwidmVyc2lvbiI6bnVsbH0sInRmd19kdXBsaWNhdGVfc2NyaWJlc190b19zZXR0aW5ncyI6eyJidWNrZXQiOiJvbiIsInZlcnNpb24iOm51bGx9LCJ0ZndfdXNlX3Byb2ZpbGVfaW1hZ2Vfc2hhcGVfZW5hYmxlZCI6eyJidWNrZXQiOiJvbiIsInZlcnNpb24iOm51bGx9LCJ0ZndfdmlkZW9faGxzX2R5bmFtaWNfbWFuaWZlc3RzXzE1MDgyIjp7ImJ1Y2tldCI6InRydWVfYml0cmF0ZSIsInZlcnNpb24iOm51bGx9LCJ0ZndfbGVnYWN5X3RpbWVsaW5lX3N1bnNldCI6eyJidWNrZXQiOnRydWUsInZlcnNpb24iOm51bGx9LCJ0ZndfdHdlZXRfZWRpdF9mcm9udGVuZCI6eyJidWNrZXQiOiJvbiIsInZlcnNpb24iOm51bGx9fQ%3D%3D&frame=false&hideCard=false&hideThread=false&id=1621617209247240193&lang=en&origin=https%3A%2F%2Fadmin.medium.seznam.cz%2F&sessionId=4a57a64ee6c06348d7baf8ed001f821db3727027&theme=light&widgetsVersion=aaf4084522e3a%3A1674595607486&width=550px\"}},{\"insert\":\"\\nO to smutnější je, když větší počet mezer vidíte i v text, který vznikl ve Wordu. Word totiž už poměrně hodně dlouho na tento jev (a mnoho dalších) upozorňuje a při najetí myši do takového místa nám v novějších verzí nabídne rovnou opravu.\\n\"},{\"insert\":{\"image\":\"//d8-a.sdn.cz/d_8/c_imgrestricted_QL_H/RAYUG/word-ms-word-editor.webp?fl=cro,0,0,1081,608|res,600,,3\"}},{\"insert\":\"\\nUkázka zvýraznění „mnohočetné“ mezery ve Wordu\\nNa CAPS LOCK šaháme, ale jen výjimečně\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"CAPS LOCK se snažíme používat co nejméně. Ideální je šáhnout po něm, jenom když píšeme velké Ě, Š, Č, Ř apod. (zkrátka všude tam, kde by nám to přes shift nešlo).\\nPokud chceme mít třeba v nadpisu všechna písmena velká, nebo použít kapitálky (kapitálky jsou zpravidla samostatný řez písma, stejně jako horní a dolní index), použijeme pro to přímo určenou funkci ve Wordu. Kdybychom totiž celý text napsali přes CAPS LOCK a v budoucnu si to rozmysleli, bude dost pravděpodobné, že půlku textu budeme nuceni přepsat.\\n\"},{\"insert\":{\"image\":\"//d8-a.sdn.cz/d_8/c_imgrestricted_QR_F/ziUk7/ms-word-word.webp?fl=cro,0,0,1920,1017|res,600,,3\"}},{\"insert\":\"\\nDetailní nabídka písmo\\nZarovnání pomocí mezerníku nebo tabulátoru? Ne díky!\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"Pořád se můžeme setkat s tím, že někdo zarovnává text pomocí mezerníku. Nejenže to je zbytečně pracné, ale když bychom změnili velikost písma takto „zarovnaného“ textu, toto zarovnání se nám kompletně rozhodí. Proto k tomu raději používejme k tomu určené nástroje (nalezneme na kartě domů v panelu odstavec).\\n\\nK nové stránce milion a jedním Enterem\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"Toto je dost častá praktika, se kterou se ze všech výše zmíněných můžeme setkat dost možná nejčastěji. Novou stránku „vytvoříme“ tak, že držíme Enter tak dlouho, než se k ní „doentrujem“.\\nStejně jako v případu zarovnávání pomocí mezerníku platí, že kdybychom někdy v budoucnu změnili velikost fontu, celé formátování se nám tím kompletně rozhodí. Proto pokud chceme na dané stránce s psaním textu skončit, přejdeme na kartu vložení a zde klikneme na tlačítko \"},{\"attributes\":{\"italic\":true},\"insert\":\"Konec stránky\"},{\"insert\":\".\\n\\nZávěrem\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"V textu jsme zmínili pouze chyby, které nadělají nejvíc škody. Rozhodně nebyly zmíněny všechny. Pokud byste se o práci s dokumenty, zejména ve Wordu, rádi dozvěděli více, můžete nás sledovat zde na Seznam Médiu nebo můžete navštívit \"},{\"attributes\":{\"link\":\"https://comptelo.com\"},\"insert\":\"náš web comptelo.com\"},{\"insert\":\".\\n\"}]}";
            string clanek = "{\"ops\":[{\"insert\":\"Při tvorbě dokumentů existují jistá pravidla, která bychom měli všichni dodržovat, už jen proto, že když je všechny dodržíme, bude se naše dílo číst mnohem lépe a i tak (mnohem lépe) bude vypadat.\\nPojďme si společně některá tato pravidla zopakovat!\\n\\nStřídmost s fonty i barvami\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"Stejně jako u střídmosti s jídlem a pitím, platí i střídmost s fonty a barvami. Jednoduše řečeno, všeho moc škodí.\\nNení výjimečné, že se v jednom dokumentu vystřídá fontů několik a barevný je jak pouťová atrakce. \\n\"},{\"insert\":{\"image\":\"//d8-a.sdn.cz/d_8/c_imgrestricted_QR_F/t68k4/mix-fontu.png?fl=cro,0,0,1920,1080|res,600,,3\"}},{\"insert\":\"\\nUkázka mixování fontů\\nJak je to s fonty?\"},{\"attributes\":{\"header\":3},\"insert\":\"\\n\"},{\"insert\":\"Na obrázku výše vidíme možný příklad míchání fontů (záměrně přehnaný). Je to takový nepěkná matlanina. Že?\\nIdeální je, nepoužívat v našem díle více než 3 fonty, ale ten čtvrtý bychom taky nějak přežili. V rozsáhlejších pracích pak používáme jeden bezpatkový (příkladem může být všeobecně známé Calibri nebo na našem \"},{\"attributes\":{\"link\":\"https://comptelo.com\"},\"insert\":\"webu\"},{\"insert\":\" používané Roboto), který budeme mít primárně vyhrazený pro nadpisy a jeden patkový (všemi známý Times New Roman nebo námi používaný Roboto Slab). Ten bude vyhrazený pro samotné „tělo“ textu.\\nVyhněme se barevném cirkusu\"},{\"attributes\":{\"header\":3},\"insert\":\"\\n\"},{\"insert\":\"Dalším častým nešvarem je používání příliš velkého počtu barev. Stejně jako u fontů platí 3 maximálně 4 barvy na dokument.\\nTaké bychom se vyhnuli tomu, aby se barvy používali pro časté zvýrazňování textu. Text sice zvýrazníte, ale při nadměrném užití se opět dostaneme do stádia, kdy je náš dokument barevný jak papoušek. Pro zvýraznění textu můžeme použít kurzívu, tučný text, nebo podtržení.\\nTip: při formátování si můžete práci ulehčit třeba pomocí stylů ve Wordu!\"},{\"attributes\":{\"blockquote\":true},\"insert\":\"\\n\"},{\"insert\":\"\\nJedna mezera stačí, drahoušku\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"Toto je bohužel stále častý jev, který se vyskytuje v nejednom příspěvku na Facebooku, síti X (v našich srdcích stále Twitteru), ročníkové práci či úředním rozhodnutí. \\n\\n\"},{\"insert\":{\"video\":\"https://platform.twitter.com/embed/Tweet.html?dnt=false&embedId=twitter-widget-2&features=eyJ0ZndfdGltZWxpbmVfbGlzdCI6eyJidWNrZXQiOltdLCJ2ZXJzaW9uIjpudWxsfSwidGZ3X2ZvbGxvd2VyX2NvdW50X3N1bnNldCI6eyJidWNrZXQiOnRydWUsInZlcnNpb24iOm51bGx9LCJ0ZndfdHdlZXRfZWRpdF9iYWNrZW5kIjp7ImJ1Y2tldCI6Im9uIiwidmVyc2lvbiI6bnVsbH0sInRmd19yZWZzcmNfc2Vzc2lvbiI6eyJidWNrZXQiOiJvbiIsInZlcnNpb24iOm51bGx9LCJ0ZndfZm9zbnJfc29mdF9pbnRlcnZlbnRpb25zX2VuYWJsZWQiOnsiYnVja2V0Ijoib24iLCJ2ZXJzaW9uIjpudWxsfSwidGZ3X21peGVkX21lZGlhXzE1ODk3Ijp7ImJ1Y2tldCI6InRyZWF0bWVudCIsInZlcnNpb24iOm51bGx9LCJ0ZndfZXhwZXJpbWVudHNfY29va2llX2V4cGlyYXRpb24iOnsiYnVja2V0IjoxMjA5NjAwLCJ2ZXJzaW9uIjpudWxsfSwidGZ3X3Nob3dfYmlyZHdhdGNoX3Bpdm90c19lbmFibGVkIjp7ImJ1Y2tldCI6Im9uIiwidmVyc2lvbiI6bnVsbH0sInRmd19kdXBsaWNhdGVfc2NyaWJlc190b19zZXR0aW5ncyI6eyJidWNrZXQiOiJvbiIsInZlcnNpb24iOm51bGx9LCJ0ZndfdXNlX3Byb2ZpbGVfaW1hZ2Vfc2hhcGVfZW5hYmxlZCI6eyJidWNrZXQiOiJvbiIsInZlcnNpb24iOm51bGx9LCJ0ZndfdmlkZW9faGxzX2R5bmFtaWNfbWFuaWZlc3RzXzE1MDgyIjp7ImJ1Y2tldCI6InRydWVfYml0cmF0ZSIsInZlcnNpb24iOm51bGx9LCJ0ZndfbGVnYWN5X3RpbWVsaW5lX3N1bnNldCI6eyJidWNrZXQiOnRydWUsInZlcnNpb24iOm51bGx9LCJ0ZndfdHdlZXRfZWRpdF9mcm9udGVuZCI6eyJidWNrZXQiOiJvbiIsInZlcnNpb24iOm51bGx9fQ%3D%3D&frame=false&hideCard=false&hideThread=false&id=1621617209247240193&lang=en&origin=https%3A%2F%2Fadmin.medium.seznam.cz%2F&sessionId=4a57a64ee6c06348d7baf8ed001f821db3727027&theme=light&widgetsVersion=aaf4084522e3a%3A1674595607486&width=550px\"}},{\"insert\":\"\\nO to smutnější je, když větší počet mezer vidíte i v text, který vznikl ve Wordu. Word totiž už poměrně hodně dlouho na tento jev (a mnoho dalších) upozorňuje a při najetí myši do takového místa nám v novějších verzí nabídne rovnou opravu.\\n\"},{\"insert\":{\"image\":\"//d8-a.sdn.cz/d_8/c_imgrestricted_QL_H/RAYUG/word-ms-word-editor.webp?fl=cro,0,0,1081,608|res,600,,3\"}},{\"insert\":\"\\nUkázka zvýraznění „mnohočetné“ mezery ve Wordu\\nNa CAPS LOCK šaháme, ale jen výjimečně\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"CAPS LOCK se snažíme používat co nejméně. Ideální je šáhnout po něm, jenom když píšeme velké Ě, Š, Č, Ř apod. (zkrátka všude tam, kde by nám to přes shift nešlo).\\nPokud chceme mít třeba v nadpisu všechna písmena velká, nebo použít kapitálky (kapitálky jsou zpravidla samostatný řez písma, stejně jako horní a dolní index), použijeme pro to přímo určenou funkci ve Wordu. Kdybychom totiž celý text napsali přes CAPS LOCK a v budoucnu si to rozmysleli, bude dost pravděpodobné, že půlku textu budeme nuceni přepsat.\\n\"},{\"insert\":{\"image\":\"//d8-a.sdn.cz/d_8/c_imgrestricted_QR_F/ziUk7/ms-word-word.webp?fl=cro,0,0,1920,1017|res,600,,3\"}},{\"insert\":\"\\nDetailní nabídka písmo\\nZarovnání pomocí mezerníku nebo tabulátoru? Ne díky!\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"Pořád se můžeme setkat s tím, že někdo zarovnává text pomocí mezerníku. Nejenže to je zbytečně pracné, ale když bychom změnili velikost písma takto „zarovnaného“ textu, toto zarovnání se nám kompletně rozhodí. Proto k tomu raději používejme k tomu určené nástroje (nalezneme na kartě domů v panelu odstavec).\\n\\nK nové stránce milion a jedním Enterem\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"Toto je dost častá praktika, se kterou se ze všech výše zmíněných můžeme setkat dost možná nejčastěji. Novou stránku „vytvoříme“ tak, že držíme Enter tak dlouho, než se k ní „doentrujem“.\\nStejně jako v případu zarovnávání pomocí mezerníku platí, že kdybychom někdy v budoucnu změnili velikost fontu, celé formátování se nám tím kompletně rozhodí. Proto pokud chceme na dané stránce s psaním textu skončit, přejdeme na kartu vložení a zde klikneme na tlačítko \"},{\"attributes\":{\"italic\":true},\"insert\":\"Konec stránky\"},{\"insert\":\".\\n\\nZávěrem\"},{\"attributes\":{\"header\":2},\"insert\":\"\\n\"},{\"insert\":\"V textu jsme zmínili pouze chyby, které nadělají nejvíc škody. Rozhodně nebyly zmíněny všechny. Pokud byste se o práci s dokumenty, zejména ve Wordu, rádi dozvěděli více, můžete nás sledovat zde na Seznam Médiu nebo můžete navštívit \"},{\"attributes\":{\"link\":\"https://comptelo.com\"},\"insert\":\"náš web comptelo.com\"},{\"insert\":\".\\n\"}]}";

            //string[] clanky = { clanek };


            ViewData["clanekString"] = clanek;


            return View();
        }

        public IActionResult Obchody()
        {
            return View();
        }

        public IActionResult Mikrovlnky()
        {
            return View();
        }

        public IActionResult Jidelny()
        {
            return View(Databaze.StravovaciZarizeni.ToList());
        }
    }
}
