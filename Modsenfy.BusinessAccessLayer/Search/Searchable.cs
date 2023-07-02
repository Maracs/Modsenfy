using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modsenfy.BusinessAccessLayer.Search;

public class Searchable
{
	private float _rate;
	public object SearchObject { get; }
	public PropertyInfo SearchProperty { get; }
	public string Query { get; }
	public float Rate
	{
		get
		{
			return _rate;
		}
	}

	public Searchable(object searchObject, PropertyInfo searchProperty, string query)
	{
		SearchObject = searchObject;
		SearchProperty = searchProperty;
		Query = query;
		_rate = CountRate();
	}

	private float CountRate()
	{
		float rate = 0f;

		var queryWords = Query.ToLowerInvariant().Split(new char[] { ' ' });
		var name = ((string)SearchProperty.GetValue(SearchObject));
		var nameWords = name.ToLowerInvariant().Split(new char[] { ' ' });

		List<int> lengthsOfHits = new List<int>();
		foreach (var nameWord in nameWords)
		{
			foreach (var queryWord in queryWords)
			{
				var qw = queryWord;
				while (!nameWord.Contains(qw) && qw.Length > 0)
				{
					qw = qw.Remove(qw.Length - 1);
				}
				lengthsOfHits.Add(qw.Length);
			}
		}

		
		foreach (var l in lengthsOfHits)
			rate += l;
        rate /= (name.Length - (nameWords.Count() - 1));
        return rate;
	}
}
