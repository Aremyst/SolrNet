﻿#region license
// Copyright (c) 2007-2009 Mauricio Scheffer
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

namespace SolrNet.Tests {
    public partial class SolrQueryResultsParserTests {
        private const string responseXmlWithHighlighting =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<lst name=""responseHeader""><int name=""status"">0</int><int name=""QTime"">15</int><lst name=""params""><str name=""hl.fl"">features,spell</str><str name=""q"">features:noise</str><str name=""hl"">true</str></lst></lst>
<result name=""response"" numFound=""1"" start=""0""><doc><arr name=""cat""><str>electronics</str><str>hard drive</str></arr><arr name=""features""><str>7200RPM, 8MB cache, IDE Ultra ATA-133</str><str>NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor</str></arr><str name=""id"">SP2514N</str><bool name=""inStock"">true</bool><str name=""manu"">Samsung Electronics Co. Ltd.</str><str name=""name"">Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133</str><int name=""popularity"">6</int><float name=""price"">92.0</float><str name=""sku"">SP2514N</str><arr name=""spell""><str>Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133</str></arr><date name=""timestamp"">0001-01-01T00:00:00Z</date><float name=""weight"">0.0</float></doc></result>
<lst name=""highlighting"">
	<lst name=""SP2514N""><arr name=""features""><str>&lt;em&gt;Noise&lt;/em&gt;Guard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor</str></arr></lst>
</lst>
</response>
";

        private const string responseXml =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<lst name=""responseHeader"">
	<int name=""status"">1</int>
	<int name=""QTime"">15</int>
	<lst name=""params"">
		<str name=""q"">id:123456</str>
		<str name=""version"">2.2</str>
	</lst>
</lst>
<result name=""response"" numFound=""1"" start=""0""><doc><str name=""advancedview""/><str name=""basicview""/><int name=""id"">123456</int></doc></result>
</response>
";

        private const string responseXMLWithArraysSimple =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<responseHeader><status>0</status><QTime>1</QTime></responseHeader>
<result numFound=""1"" start=""0"" maxScore=""1.6578954"">
	<doc>
		<arr name=""features""><str>7200RPM, 8MB cache, IDE Ultra ATA-133</str><str>NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor</str></arr>
	</doc>
</result>
</response>
";

        private const string responseXMLWithArrays =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<responseHeader><status>0</status><QTime>1</QTime></responseHeader>

<result numFound=""1"" start=""0"">
 <doc>
  <arr name=""cat""><str>electronics</str><str>hard drive</str></arr>
  <arr name=""features""><str>7200RPM, 8MB cache, IDE Ultra ATA-133</str><str>NoiseGuard, SilentSeek technology, Fluid Dynamic Bearing (FDB) motor</str></arr>
  <str name=""id"">SP2514N</str>
  <bool name=""inStock"">true</bool>
  <str name=""manu"">Samsung Electronics Co. Ltd.</str>
  <str name=""name"">Samsung SpinPoint P120 SP2514N - hard drive - 250 GB - ATA-133</str>
  <int name=""popularity"">6</int>
  <float name=""price"">92.0</float>
  <str name=""sku"">SP2514N</str>
	<arr name=""numbers""><int>1</int><int>2</int></arr>
 </doc>
</result>
</response>
";

        private const string responseXMLWithDate = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<result numFound=""1"" start=""0"">
	<doc>
	<date name=""Fecha"">2001-01-02T03:04:05Z</date>
	</doc>
</result>
</response>
";

        private const string responseXMLWithGuid =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<result numFound=""1"" start=""0"">
	<doc>
	<str name=""Key"">224fbdc1-12df-4520-9fbe-dd91f916eba1</str>
	</doc>
</result>
</response>
";

        private const string responseXMLWithFacet =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<responseHeader><status>0</status><QTime>2</QTime></responseHeader>
<result numFound=""4"" start=""0""/>
<lst name=""facet_counts"">
 <lst name=""facet_queries"">
	<int name=""payment:[0 TO 1000]"">259</int>
 </lst>
 <lst name=""facet_fields"">
  <lst name=""cat"">
        <int name=""search"">0</int>
        <int name=""memory"">0</int>
        <int name=""graphics"">0</int>
        <int name=""card"">0</int>
        <int name=""music"">1</int>
        <int name=""software"">0</int>
        <int name=""electronics"">3</int>
        <int name=""copier"">0</int>
        <int name=""multifunction"">0</int>
        <int name=""camera"">0</int>
        <int name=""connector"">2</int>
        <int name=""hard"">0</int>
        <int name=""scanner"">0</int>
        <int name=""monitor"">0</int>
        <int name=""drive"">0</int>
        <int name=""printer"">0</int>
  </lst>
  <lst name=""inStock"">
        <int name=""false"">3</int>
        <int name=""true"">1</int>
  </lst>
 </lst>
</lst>
</response>";

        private const string responseXMLWithEnumAsInt = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<result numFound=""1"" start=""0"">
	<doc>
	<int name=""En"">1</int>
	</doc>
</result>
</response>
";

        private const string responseXMLWithEnumAsString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<result numFound=""1"" start=""0"">
	<doc>
	<int name=""En"">Two</int>
	</doc>
</result>
</response>
";


        private const string responseXmlWithSpellChecking = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<response>
<result numFound=""1"" start=""0"">
	<doc>
	<str name=""Key"">224fbdc1-12df-4520-9fbe-dd91f916eba1</str>
	</doc>
</result>
<lst name=""spellcheck"">
    <lst name=""suggestions"">
        <lst name=""hell"">
            <int name=""numFound"">1</int>
            <int name=""startOffset"">0</int>
            <int name=""endOffset"">4</int>
            <arr name=""suggestion"">
                <str>dell</str>
            </arr>
        </lst>
        <lst name=""ultrashar"">
            <int name=""numFound"">1</int>
            <int name=""startOffset"">5</int>
            <int name=""endOffset"">14</int>
            <arr name=""suggestion"">
                <str>ultrasharp</str>
            </arr>
        </lst>
        <str name=""collation"">dell ultrasharp</str>
    </lst>
</lst>
</response>
";
    }
}